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
        protected ILogger _logger;

        protected PlantsInfo _generatorSettings;

        public AbstractTreeGeneratorGrain(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AbstractTreeGeneratorGrain>();
        }

        public override Task OnActivateAsync()
        {
            try
            {
                var settings = this.GetPrimaryKeyString();
                _generatorSettings = JsonConvert.DeserializeObject<PlantsInfo>(settings);
            }
            catch (Exception e)
            {
                this._logger.LogError(default(EventId), e, e.Message);
            }

            return Task.CompletedTask;
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
