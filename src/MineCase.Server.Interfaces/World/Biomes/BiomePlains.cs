using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World.Biomes
{
    public class BiomePlains : Biome
    {
        public BiomePlains(BiomeProperties properties)
            : base(properties)
        {
        }

        // ʹ��ֲ�����װ������
        public override void Decorate(IWorld world, Random rand, BlockPos pos)
        {
            // �Ӳ�

            // �ӻ�

            // ����
        }

        // ʹ�û�װ��
        private void GenFlowers()
        {

        }
    }
}