using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Settings;
using MineCase.Server.World.Generation;
using Orleans;

namespace MineCase.Server.World
{
    internal class ChunkColumnGrain : Grain, IChunkColumn
    {
        private IWorld _world;
        private int _chunkX;
        private int _chunkZ;

        private ChunkColumnStorage _state;
        private Dictionary<IPlayer, Vector3> _players;

        public async Task AttachPlayer(IPlayer player)
        {
            _players.Add(player, await player.GetPosition());
        }

        public Task DetachPlayer(IPlayer player)
        {
            _players.Remove(player);
            return Task.CompletedTask;
        }

        public Task<BlockState> GetBlockState(int x, int y, int z) => Task.FromResult(_state[x, y, z]);

        public Task<IReadOnlyCollection<(IPlayer player, Vector3 position)>> GetPlayers() =>
            Task.FromResult<IReadOnlyCollection<(IPlayer player, Vector3 position)>>(_players.Select(o => (o.Key, o.Value)).ToList());

        public Task<ChunkColumnStorage> GetState() => Task.FromResult(_state);
        /*
        var generator = GrainFactory.GetGrain<IChunkGeneratorOverworld>(1);
        GeneratorSettings settings = new GeneratorSettings
        {
            Seed = 1,
        };
        ChunkColumn chunkColumn = await generator.Generate(_chunkX, _chunkZ, settings);
        return chunkColumn;
        */

        /*
        var blocks = new Block[16 * 16 * 16];
        var index = 0;
        for (int y = 0; y < 16; y++)
        {
            for (int x = 0; x < 16; x++)
            {
                for (int z = 0; z < 16; z++)
                {
                    if (y == 0)
                        blocks[index] = new Block { Id = 1, SkyLight = 0xF };
                    else
                        blocks[index] = new Block { Id = 0, SkyLight = 0xF };
                    index++;
                }
            }
        }

        return Task.FromResult(new ChunkColumn
        {
            Biomes = Enumerable.Repeat<byte>(0, 256).ToArray(),
            SectionBitMask = 0b1111_1111_1111_1111,
            Sections = new[]
            {
                new ChunkSection
                {
                    BitsPerBlock = 13,
                    Blocks = blocks
                }
            }.Concat(Enumerable.Repeat(
                new ChunkSection
                {
                    BitsPerBlock = 13,
                    Blocks = Enumerable.Repeat(new Block { Id = 0, SkyLight = 0xF }, 16 * 16 * 16).ToArray()
                }, 15)).ToArray()
        });
        */

        public override async Task OnActivateAsync()
        {
            var key = this.GetWorldAndChunkPosition();
            _world = GrainFactory.GetGrain<IWorld>(key.worldKey);
            _chunkX = key.x;
            _chunkZ = key.z;
            _players = new Dictionary<IPlayer, Vector3>();

            await EnsureChunkGenerated();
        }

        public Task SetBlockState(int x, int y, int z, BlockState blockState)
        {
            _state[x, y, z] = blockState;
            return Task.CompletedTask;
        }

        public Task UpdatePlayerPosition(IPlayer player, Vector3 position)
        {
            _players[player] = position;
            return Task.CompletedTask;
        }

        private async Task EnsureChunkGenerated()
        {
            var serverSetting = GrainFactory.GetGrain<IServerSettings>(0);
            string worldType = (await serverSetting.GetSettings()).LevelType;
            if (worldType == "DEFAULT" || worldType == "default")
            {
                var generator = GrainFactory.GetGrain<IChunkGeneratorOverworld>(await _world.GetSeed());
                GeneratorSettings settings = new GeneratorSettings
                {
                };
                _state = await generator.Generate(_world, _chunkX, _chunkZ, settings);
            }
            else if (worldType == "FLAT" || worldType == "flat")
            {
                var generator = GrainFactory.GetGrain<IChunkGeneratorFlat>(await _world.GetSeed());
                GeneratorSettings settings = new GeneratorSettings
                {
                    FlatBlockId = new BlockState?[] { BlockStates.Stone(), BlockStates.Dirt(), BlockStates.Grass() }
                };
                _state = await generator.Generate(_world, _chunkX, _chunkZ, settings);
            }
            else
            {
                var generator = GrainFactory.GetGrain<IChunkGeneratorOverworld>(await _world.GetSeed());
                GeneratorSettings settings = new GeneratorSettings
                {
                };
                _state = await generator.Generate(_world, _chunkX, _chunkZ, settings);
            }
        }
    }
}
