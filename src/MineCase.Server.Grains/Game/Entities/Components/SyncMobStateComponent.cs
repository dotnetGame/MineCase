using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Game.Entities.Components
{
    internal class SyncMobStateComponent : Component<EntityGrain>, IHandle<SpawnMob>
    {
        public SyncMobStateComponent(string name = "syncPlayerState")
            : base(name)
        {
        }

        Task IHandle<SpawnMob>.Handle(SpawnMob message)
        {
            InstallPropertyChangedHandlers();
            return Task.CompletedTask;
        }

        private void InstallPropertyChangedHandlers()
        {
            AttachedObject.RegisterPropertyChangedHandler(EntityLookComponent.YawProperty, OnYawChanged);
            AttachedObject.RegisterPropertyChangedHandler(EntityLookComponent.PitchProperty, OnPitchChanged);
        }

        private Task OnYawChanged(object sender, PropertyChangedEventArgs<float> e)
        {
            uint eid = AttachedObject.GetValue(EntityIdComponent.EntityIdProperty);
            byte yaw = (byte)(AttachedObject.GetValue(EntityLookComponent.YawProperty) / 360 * 255);
            return AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator().EntityHeadLook(eid, yaw);
        }

        private Task OnPitchChanged(object sender, PropertyChangedEventArgs<float> e)
        {
            uint eid = AttachedObject.GetValue(EntityIdComponent.EntityIdProperty);
            byte yaw = (byte)(AttachedObject.GetValue(EntityLookComponent.YawProperty) / 360 * 255);
            byte pitch = (byte)(AttachedObject.GetValue(EntityLookComponent.PitchProperty) / 360 * 255);
            bool onGround = AttachedObject.GetValue(EntityOnGroundComponent.IsOnGroundProperty);
            return AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator().EntityLook(eid, yaw, pitch, onGround);
        }
    }
}
