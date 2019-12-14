using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase
{
    public static class BoundingBox
    {
        public static Vector3 Item()
        {
            return new Vector3
            {
                X = 0.25f,
                Z = 0.25f,
                Y = 0.25f
            };
        }

        public static Vector3 XPOrb()
        {
            return new Vector3
            {
                X = 0.5f,
                Z = 0.5f,
                Y = 0.5f
            };
        }

        public static Vector3 AreaEffectCloud(float radius)
        {
            return new Vector3
            {
                X = 2.0f * radius,
                Z = 2.0f * radius,
                Y = 0.5f
            };
        }

        public static Vector3 ElderGuardian()
        {
            return new Vector3
            {
                X = 1.9975f,
                Z = 1.9975f,
                Y = 1.9975f
            };
        }

        public static Vector3 WitherSkeleton()
        {
            return new Vector3
            {
                X = 0.7f,
                Z = 0.7f,
                Y = 2.4f
            };
        }

        public static Vector3 Stray()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 1.99f
            };
        }

        public static Vector3 ThrownEgg()
        {
            return new Vector3
            {
                X = 0.25f,
                Z = 0.25f,
                Y = 0.25f
            };
        }

        public static Vector3 LeashKnot()
        {
            return new Vector3
            {
                X = 0.375f,
                Z = 0.375f,
                Y = 0.5f
            };
        }

        public static Vector3 Painting()
        {
            return new Vector3
            {
                X = 0f, // TODO: type width or 0.0625 (depth),
                Z = 0f, // TODO: type width or 0.0625 (depth),
                Y = 0f, // TODO: type height
            };
        }

        public static Vector3 Arrow()
        {
            return new Vector3
            {
                X = 0.5f,
                Z = 0.5f,
                Y = 0.5f
            };
        }

        public static Vector3 Snowball()
        {
            return new Vector3
            {
                X = 0.25f,
                Z = 0.25f,
                Y = 0.25f
            };
        }

        public static Vector3 Fireball()
        {
            return new Vector3
            {
                X = 1.0f,
                Z = 1.0f,
                Y = 1.0f
            };
        }

        public static Vector3 SmallFireball()
        {
            return new Vector3
            {
                X = 0.3125f,
                Z = 0.3125f,
                Y = 0.3125f
            };
        }

        public static Vector3 ThrownEnderpearl()
        {
            return new Vector3
            {
                X = 0.25f,
                Z = 0.25f,
                Y = 0.25f
            };
        }

        public static Vector3 EyeOfEnderSignal()
        {
            return new Vector3
            {
                X = 0.25f,
                Z = 0.25f,
                Y = 0.25f
            };
        }

        public static Vector3 ThrownPotion()
        {
            return new Vector3
            {
                X = 0.25f,
                Z = 0.25f,
                Y = 0.25f
            };
        }

        public static Vector3 ThrownExpBottle()
        {
            return new Vector3
            {
                X = 0.25f,
                Z = 0.25f,
                Y = 0.25f
            };
        }

        public static Vector3 ItemFrame()
        {
            return new Vector3
            {
                X = 0f, // TODO: 0.75 or 0.0625 (depth),
                Z = 0f, // TODO: 0.75 or 0.0625 (depth),
                Y = 0.75f
            };
        }

        public static Vector3 WitherSkull()
        {
            return new Vector3
            {
                X = 0.3125f,
                Z = 0.3125f,
                Y = 0.3125f
            };
        }

        public static Vector3 PrimedTnt()
        {
            return new Vector3
            {
                X = 0.98f,
                Z = 0.98f,
                Y = 0.98f
            };
        }

        public static Vector3 FallingSand()
        {
            return new Vector3
            {
                X = 0.98f,
                Z = 0.98f,
                Y = 0.98f
            };
        }

        public static Vector3 FireworksRocketEntity()
        {
            return new Vector3
            {
                X = 0.25f,
                Z = 0.25f,
                Y = 0.25f
            };
        }

        public static Vector3 Husk()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 1.95f
            };
        }

        public static Vector3 SpectralArrow()
        {
            return new Vector3
            {
                X = 0.5f,
                Z = 0.5f,
                Y = 0.5f
            };
        }

        public static Vector3 ShulkerBullet()
        {
            return new Vector3
            {
                X = 0.3125f,
                Z = 0.3125f,
                Y = 0.3125f
            };
        }

        public static Vector3 DragonFireball()
        {
            return new Vector3
            {
                X = 1.0f,
                Z = 1.0f,
                Y = 1.0f
            };
        }

        public static Vector3 ZombieVillager()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 1.95f
            };
        }

        public static Vector3 SkeletonHorse()
        {
            return new Vector3
            {
                X = 1.3964844f,
                Z = 1.3964844f,
                Y = 1.6f
            };
        }

        public static Vector3 ZombieHorse()
        {
            return new Vector3
            {
                X = 1.3964844f,
                Z = 1.3964844f,
                Y = 1.6f
            };
        }

        public static Vector3 ArmorStand()
        {
            return new Vector3
            {
                X = 0f, // TODO: normal: 0.5 marker: 0.0 small: 0.25,
                Z = 0f, // TODO: normal: 0.5 marker: 0.0 small: 0.25,
                Y = 0f, // TODO: normal: 1.975 marker: 0.0 small: 0.9875
            };
        }

        public static Vector3 Donkey()
        {
            return new Vector3
            {
                X = 1.3964844f,
                Z = 1.3964844f,
                Y = 1.6f
            };
        }

        public static Vector3 Mule()
        {
            return new Vector3
            {
                X = 1.3964844f,
                Z = 1.3964844f,
                Y = 1.6f
            };
        }

        public static Vector3 EvocationFangs()
        {
            return new Vector3
            {
                X = 0.5f,
                Z = 0.5f,
                Y = 0.8f
            };
        }

        public static Vector3 EvocationIllager()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 1.95f
            };
        }

        public static Vector3 Vex()
        {
            return new Vector3
            {
                X = 0.4f,
                Z = 0.4f,
                Y = 0.8f
            };
        }

        public static Vector3 VindicationIllager()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 1.95f
            };
        }

        public static Vector3 IllusionIllager()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 1.95f
            };
        }

        public static Vector3 MinecartCommandBlock()
        {
            return new Vector3
            {
                X = 0.98f,
                Z = 0.98f,
                Y = 0.7f
            };
        }

        public static Vector3 Boat()
        {
            return new Vector3
            {
                X = 1.375f,
                Z = 1.375f,
                Y = 0.5625f
            };
        }

        public static Vector3 MinecartRideable()
        {
            return new Vector3
            {
                X = 0.98f,
                Z = 0.98f,
                Y = 0.7f
            };
        }

        public static Vector3 MinecartChest()
        {
            return new Vector3
            {
                X = 0.98f,
                Z = 0.98f,
                Y = 0.7f
            };
        }

        public static Vector3 MinecartFurnace()
        {
            return new Vector3
            {
                X = 0.98f,
                Z = 0.98f,
                Y = 0.7f
            };
        }

        public static Vector3 MinecartTNT()
        {
            return new Vector3
            {
                X = 0.98f,
                Z = 0.98f,
                Y = 0.7f
            };
        }

        public static Vector3 MinecartHopper()
        {
            return new Vector3
            {
                X = 0.98f,
                Z = 0.98f,
                Y = 0.7f
            };
        }

        public static Vector3 MinecartSpawner()
        {
            return new Vector3
            {
                X = 0.98f,
                Z = 0.98f,
                Y = 0.7f
            };
        }

        public static Vector3 Creeper()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 1.7f
            };
        }

        public static Vector3 Skeleton()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 1.99f
            };
        }

        public static Vector3 Spider()
        {
            return new Vector3
            {
                X = 1.4f,
                Z = 1.4f,
                Y = 0.9f
            };
        }

        public static Vector3 Giant()
        {
            return new Vector3
            {
                X = 3.6f,
                Z = 3.6f,
                Y = 10.8f
            };
        }

        public static Vector3 Zombie()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 1.95f
            };
        }

        public static Vector3 Slime(float size)
        {
            return new Vector3
            {
                X = 0.51000005f * size,
                Z = 0.51000005f * size,
                Y = 0.51000005f * size
            };
        }

        public static Vector3 Ghast()
        {
            return new Vector3
            {
                X = 4f,
                Z = 4f,
                Y = 4f
            };
        }

        public static Vector3 PigZombie()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 1.95f
            };
        }

        public static Vector3 Enderman()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 2.9f
            };
        }

        public static Vector3 CaveSpider()
        {
            return new Vector3
            {
                X = 0.7f,
                Z = 0.7f,
                Y = 0.5f
            };
        }

        public static Vector3 Silverfish()
        {
            return new Vector3
            {
                X = 0.4f,
                Z = 0.4f,
                Y = 0.3f
            };
        }

        public static Vector3 Blaze()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 1.8f
            };
        }

        public static Vector3 LavaSlime(float size)
        {
            return new Vector3
            {
                X = 0.51000005f * size,
                Z = 0.51000005f * size,
                Y = 0.51000005f * size
            };
        }

        public static Vector3 EnderDragon()
        {
            return new Vector3
            {
                X = 16.0f,
                Z = 16.0f,
                Y = 8.0f
            };
        }

        public static Vector3 WitherBoss()
        {
            return new Vector3
            {
                X = 0.9f,
                Z = 0.9f,
                Y = 3.5f
            };
        }

        public static Vector3 Bat()
        {
            return new Vector3
            {
                X = 0.5f,
                Z = 0.5f,
                Y = 0.9f
            };
        }

        public static Vector3 Witch()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 1.95f
            };
        }

        public static Vector3 Endermite()
        {
            return new Vector3
            {
                X = 0.4f,
                Z = 0.4f,
                Y = 0.3f
            };
        }

        public static Vector3 Guardian()
        {
            return new Vector3
            {
                X = 0.85f,
                Z = 0.85f,
                Y = 0.85f
            };
        }

        public static Vector3 Shulker()
        {
            return new Vector3
            {
                X = 1.0f,
                Z = 1.0f,
                Y = 0f // TODO: 1.0-2.0 (depending on peek)
            };
        }

        public static Vector3 Pig()
        {
            return new Vector3
            {
                X = 0.9f,
                Z = 0.9f,
                Y = 0.9f
            };
        }

        public static Vector3 Sheep()
        {
            return new Vector3
            {
                X = 0.9f,
                Z = 0.9f,
                Y = 1.3f
            };
        }

        public static Vector3 Cow()
        {
            return new Vector3
            {
                X = 0.9f,
                Z = 0.9f,
                Y = 1.4f
            };
        }

        public static Vector3 Chicken()
        {
            return new Vector3
            {
                X = 0.4f,
                Z = 0.4f,
                Y = 0.7f
            };
        }

        public static Vector3 Squid()
        {
            return new Vector3
            {
                X = 0.8f,
                Z = 0.8f,
                Y = 0.8f
            };
        }

        public static Vector3 Wolf()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 0.85f
            };
        }

        public static Vector3 MushroomCow()
        {
            return new Vector3
            {
                X = 0.9f,
                Z = 0.9f,
                Y = 1.4f
            };
        }

        public static Vector3 SnowMan()
        {
            return new Vector3
            {
                X = 0.7f,
                Z = 0.7f,
                Y = 1.9f
            };
        }

        public static Vector3 Ozelot()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 0.7f
            };
        }

        public static Vector3 VillagerGolem()
        {
            return new Vector3
            {
                X = 1.4f,
                Z = 1.4f,
                Y = 2.7f
            };
        }

        public static Vector3 Horse()
        {
            return new Vector3
            {
                X = 1.3964844f,
                Z = 1.3964844f,
                Y = 1.6f
            };
        }

        public static Vector3 Rabbit()
        {
            return new Vector3
            {
                X = 0.4f,
                Z = 0.4f,
                Y = 0.5f
            };
        }

        public static Vector3 PolarBear()
        {
            return new Vector3
            {
                X = 1.3f,
                Z = 1.3f,
                Y = 1.4f
            };
        }

        public static Vector3 Llama()
        {
            return new Vector3
            {
                X = 0.9f,
                Z = 0.9f,
                Y = 1.87f
            };
        }

        public static Vector3 LlamaSpit()
        {
            return new Vector3
            {
                X = 0.25f,
                Z = 0.25f,
                Y = 0.25f
            };
        }

        public static Vector3 Parrot()
        {
            return new Vector3
            {
                X = 0.5f,
                Z = 0.5f,
                Y = 0.9f
            };
        }

        public static Vector3 Villager()
        {
            return new Vector3
            {
                X = 0.6f,
                Z = 0.6f,
                Y = 1.95f
            };
        }

        public static Vector3 EnderCrystal()
        {
            return new Vector3
            {
                X = 2.0f,
                Z = 2.0f,
                Y = 2.0f
            };
        }
    }
}
