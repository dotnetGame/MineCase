# Terrain

Before introducing terrain generation, I think it is necessary to introduce the basic concepts related to terrain in minecraft.



## Basic Concept

**Coordinate**

**Coordinates represent the player's position in the Minecraft world in **numbers. They are based on a grid where three lines or axes intersect at the origin. The player was originally born within a few hundred blocks of the origin.

- **X** - Shows your East/West position on the map. Positive numbers indicate East. Negative numbers indicate West.
- **Y** - Shows your altitude on the map. The integer indicates that it is on the ground. The negative number indicates that it is below the ground. (From 0 to 255, where 64 is sea level)
- **Z** - Shows your location on the map South/North. Positive numbers indicate south. Negative numbers indicate north.

Thus forming a [right-handed coordinate system](https://en.wikipedia.org/wiki/Right-hand_rule)  (thumb≡x, index≡y, middle≡z)

MineCase's source code includes BlockWorldPos, BlockChunkPos and ChunkWorldPos, etc., which represent coordinate classes (MineCase\src\MineCase.Core\Position.cs), which represent the coordinates of Block in World, the coordinates of Block in Chunk and the coordinates of Chunk in World. .



**Minecraft Data Storage**

Minecraft's map is composed of one block, 16x16x16 blocks form a section, 16 sections are vertically arranged to form a chunk, and the size of the chunk is 16x16x256. The chunk data that has been generated is stored in the file, and the ungenerated ones are not stored.

Minekase stores Chunk data (MineCase\src\MineCase.Core\World\ChunkColumnStorage.cs) by ChunkColumnStorage and ChunkColumnCompactStorage.

They all implement the IChunkColumnStorage interface, but the former is uncompressed data, the latter is compressed data, the former is used in the terrain generation process, because the former will be accessed faster, in the Chunk The latter is used after generation, and it does not need to be compressed again when transmitting, saving time.



**Block Data**

Each block data mainly includes block id and meta data, of course, there will be skylight and blocklight data during the transmission.

The blockid is used to distinguish between different types of squares. The metavalue is used to distinguish the specific values of each type of block, such as a burning furnace and a common furnace. For example, the orientation of the square.

In the MineCase source code, BlockState represents the type of the box (MineCase.Core\Block\BlockState.cs)

BlockStates is used to create the type of box (MineCase.Core\Block\BlockStates.cs)

Block is used to describe the different behaviors of different types of blocks (MineCase.Core\Block\Block.cs)



**Chunk Data**

Each chunk of data contains a 16x16x256 block, which is also the basic unit for map transfer and storage. After the user logs in to the server, the server will pass the chunk around the user's point to the client.
