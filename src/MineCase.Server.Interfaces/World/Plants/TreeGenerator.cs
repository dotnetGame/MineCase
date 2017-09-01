﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Biomes;
using Orleans;

namespace MineCase.Server.World.Plants
{
    public class TreeGenerator : PlantsGenerator
    {
        private int _minTreeHeight;

        private bool _vines;

        private BlockState _wood;

        private BlockState _leaves;

        public TreeGenerator(int treeHeight, bool vines, BlockState wood, BlockState leaves)
        {
            _minTreeHeight = treeHeight;
            _vines = vines;
            _wood = wood;
            _leaves = leaves;
        }

        public override Task Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            return Task.CompletedTask;
        }
    }
}