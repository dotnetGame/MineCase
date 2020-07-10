# minecase地形生成分析

地形

---

## Concept 基本概念
地形生成的代码主要在net/Minecraft/world文件夹下，地形的生成主要分为两个阶段，generate和populate。generate主要进行基本地形生成，生物群落特有方块覆盖，以及skylight计算。populate主要负责建筑生成，植物生成，动物生成等地形附加结构。


## minecraft数据基本存储结构
minecraft的地图数据存储的基本元素称为block，16x16x16个block组成一个section，16个section垂直排列组成一个chunk，chunk也就是我们常说的区块，区块的大小是16x16x256。chunk数据以nbt格式存储在文件中（nbt以后有空讲）

## block数据

每个block数据主要包括block id和meta数据，当然在传输过程中还会有skylight数据。
blockid用来区分区分不同种类的方块，metavalue用来区分每一大类方块具体数值，例如燃烧着的熔炉和普通熔炉。例如方块的朝向。


## chunk数据

每个chunk的数据包含了16x16x256的block，它也是地图传输与存储的基本单位。用户登入服务器后，服务器会将user所在点周围chunk传给客户端。

## minecraft坐标系统

对于minecraft中坐标，玩家们应该都很熟悉，xyz中y轴是高度，x，z轴与地面水平。
接下来我来介绍一下mc中几个常用的坐标
1．	Entity在世界中的坐标，共三个分量，xyz，类型float
2．	Block在世界中的坐标，共三个分量，xyz，类型int
3．	Block在chunk中的坐标，三个分量，xyz，类型int
4．	Chunk在世界中坐标，共两个分量，xz，类型int

## 坐标转换

Block世界坐标转chunk内坐标
```cs
    public BlockChunkPos ToBlockChunkPos()
    {
        int blockPosX = X % ChunkConstants.BlockEdgeWidthInSection;
        int blockPosZ = Z % ChunkConstants.BlockEdgeWidthInSection;
        if (blockPosX < 0) blockPosX += ChunkConstants.BlockEdgeWidthInSection;
        if (blockPosZ < 0) blockPosZ += ChunkConstants.BlockEdgeWidthInSection;
        return new BlockChunkPos(blockPosX, Y, blockPosZ);
    }
```

Block世界坐标转chunk坐标
```cs
    public ChunkWorldPos ToChunkWorldPos()
    {
        int chunkPosX = X / ChunkConstants.BlockEdgeWidthInSection;
        int chunkPosZ = Z / ChunkConstants.BlockEdgeWidthInSection;
        if (chunkPosX < 0) chunkPosX -= 1;
        if (chunkPosZ < 0) chunkPosZ -= 1;
        return new ChunkWorldPos(chunkPosX, chunkPosZ);
    }
```

Chunk坐标转block在世界中坐标
```cs
public BlockWorldPos ToBlockWorldPos()
{
    return new BlockWorldPos(X * 16, 0, Z * 16);
}
```


Entity在世界中坐标转block在世界中坐标

```cs
public BlockWorldPos ToBlockWorldPos()
{
    int x = (int)Math.Floor(X);
    int y = (int)Math.Floor(Y);
    int z = (int)Math.Floor(Z);
    return new BlockWorldPos(x, y, z);
}

```

## ChunkGenerator 地形生成器
mc的地形生成器内主要有两个方法，一个是generate，一个是populate，分别对应着生成过程中的两个阶段，在IChunkGenerator中可以很清楚得看到

```cs
public interface IChunkGenerator
{
    Task<ChunkColumnCompactStorage> Generate(IWorld world, int x, int z, GeneratorSettings settings);
}
```

所有的ChunkGeneratorGrain都会实现此接口

我们主要会对ChunkGeneratorOverWorld进行讲解

## Biome 生物群落

生物群落是MC中的一个很重要的概念，生物群落贯穿着MC地形生成的整个过程。在generate阶段它会影响地形，影响表层覆盖的方块。在populate阶段，biome影响着地表植物的分布，动物的生成。一个chunk有16x16的biome数据，垂直方向的方块共享一个biome。Biome由一个biome ID表示，具体的ID值可以在wiki上查到。


## GenLayer 层次生成器
对于Biome的生成，MC采用了Genlayer的方式，以decorator模式，把生物群落得生成过程串在一起，从上向下逐层采样，生成Biome。
我们可以看到在Genlayer.java中将各个Genlayer串在一起
每一个new出来的layer都把上一行的layer设置为parent

在调用getInts时，每一个layer都会递归的先将parent的ints取得，进行修改后返回。

下面是几个主要的genlayer子类。

首先是GenLayerIsland，这个layer主要用来生成基本的海洋和陆地的biome分布，1是plains biome，0是ocean biome



仅靠上面的getInts获得的biome范围会很小，几乎就是每个biome块只有一格大小，我们还需要对这个数据xz放大后使用，这个工作在GenLayerZoom中完成。


GenLayerBiome用来在已有的biome中添加一些不同的Biome

先讲以上这三个主要的biome genlayer，因为genlayer实在太多了，全讲的话，一下子也理解不了。所以先讲这三个，对后面的理解有帮助。


## Terrain 基本地形
进入generate方法,下面就是generateChunk方法的前一部分。



首先我们进入setBlocksInChunk函数一探究竟。这个函数用来通过柏林噪声函数，生成一个只有石头和水的地表不平坦的世界。


先看第一行，第一行的作用是获取周围32x32的biome值，
也许有人有疑问了，在mc中一个chunk是16x16，为什么要获取32x32的biome呢？
其实获取32x32范围的biome是用来做插值的，之前我们说过了，biome会影响地形的高度，我们知道plain是比较低的，而hill是比较高的，如果正好两个biome相邻，不做特殊的处理的话，biome和biome间会有陡崖。所以我们需要周围的biome的信息以便进行插值保证地形的平滑。

不做插值的后果：



如果有人认真地复现了算法，就会有人提问了，为什么我看到返回的biome数组是10x10的呢？
这就涉及到了，mc在地形生成的一个优化，一个chunk虽然是16x16的，但是在生成高度图的时候，mc生成的是5x5x33的数组，然后对数组进行三个方向的线性插值，获得16x16x256的chunk数组。因此我们biome也只要10x10的大小（16->32, 5->10），这大大减少了运算量。

接下来我们来看generateHeightMap函数，




## Perlin Noise柏林噪声

