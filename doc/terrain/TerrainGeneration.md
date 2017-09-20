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