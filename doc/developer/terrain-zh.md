# 地形

在介绍地形生成前，我觉得有必要介绍一下minecraft中和地形相关的基本概念

## 基本概念

**坐标**

**坐标以**数字方式表示玩家在Minecraft 世界中的位置。它们基于网格，其中三条线或轴在原点处相交。玩家最初在原点的几百块内出生。

- **X** - 显示你在地图上的 东/西 位置. 正数表示东.负数表示西.
- **Y** - 显示你在地图上的海拔高度. 整数表示位于地面上.负数表示位于地面下.（从0到255，其中64是海平面）
- **Z** - 显示你在地图上的 南/北 位置. 正数表示南.负数表示北.

从而形成[右手坐标系](https://en.wikipedia.org/wiki/Right-hand_rule)（thumb≡x，index≡y，middle≡z）

MineCase的源码中有BlockWorldPos，BlockChunkPos和ChunkWorldPos等表示坐标类（MineCase\src\MineCase.Core\Position.cs），分别表示Block在World中的坐标，Block在Chunk中的坐标和Chunk在World中的坐标。



**minecraft数据基本存储结构**

Minecraft的地图是由一个一个block组成，16x16x16个block组成一个section，16个section垂直排列组成一个chunk，chunk的大小是16x16x256。已经生成的chunk数据存储在文件中，未生成的不会被存储。

MineCase中由ChunkColumnStorage和ChunkColumnCompactStorage来存储Chunk的数据（MineCase\src\MineCase.Core\World\ChunkColumnStorage.cs）。

它们都实现了IChunkColumnStorage接口，但前者是未压缩的数据，后者是压缩过的数据，在地形生成的过程中用的是前者，因为要修改大量方块，前者的访问速度会更快，在Chunk生成后使用的是后者，传输的时候就不需要再次压缩，节约时间。



**block数据**

每个block数据主要包括block id和meta数据，当然在传输过程中还会有skylight和blocklight数据。

blockid用来区分区分不同种类的方块，metavalue用来区分每类方块具体数值，例如燃烧着的熔炉和普通熔炉。例如方块的朝向。

在MineCase源码中，BlockState表示方块的种类（MineCase.Core\Block\BlockState.cs），

BlockStates用于创建方块种类(MineCase.Core\Block\BlockStates.cs)，

Block用于描述不同种类Block的不同行为(MineCase.Core\Block\Block.cs)。



**chunk数据**

每个chunk的数据包含了16x16x256的block，它也是地图传输与存储的基本单位。用户登入服务器后，服务器会将user所在点周围chunk传给客户端。


## 地形生成

**生成阶段**
地形的生成主要分为两个阶段，generate和populate。
generate主要进行基本地形生成，生物群落特有方块覆盖，以及基础的skylight计算。
populate主要负责建筑生成，植物生成，动物生成等地形附加结构。例如矿道，神庙，湖泊等。


**generate阶段**


