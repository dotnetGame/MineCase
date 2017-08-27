using System;

namespace MineCase.Server.World.Generation
{
    public class GeneratorSettings
    {
        public bool GenerateStructure{ get; set; }


        public FlatGeneratorInfo flatGeneratorInfo{get; set;}

        public OverworldGeneratorInfo OverworldGeneratorInfo{get; set;}
    }
}
