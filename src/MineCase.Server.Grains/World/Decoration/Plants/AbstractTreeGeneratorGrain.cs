using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MineCase.Block;
using MineCase.World.Plants;
using Newtonsoft.Json;
using Orleans;

namespace MineCase.Server.World.Decoration.Plants
{
    public abstract class AbstractTreeGeneratorGrain : PlantsGeneratorGrain, IAbstractTreeGenerator
    {
        protected BlockState _wood;
        protected BlockState _leaves;

        public AbstractTreeGeneratorGrain(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AbstractTreeGeneratorGrain>();
        }

        public async override Task OnActivateAsync()
        {
            await base.OnActivateAsync();
        }

        public static bool IsSoil(BlockState state)
        {
            if (state == BlockStates.Dirt() ||
                state == BlockStates.GrassBlock())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CanSustainTree(PlantsType type, BlockState state)
        {
            if (state == BlockStates.Dirt() ||
                state == BlockStates.GrassBlock())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsReplaceable(BlockState state)
        {
            return state.IsAir() || state.IsLeaves() || state.IsWood();
        }
    }
}
