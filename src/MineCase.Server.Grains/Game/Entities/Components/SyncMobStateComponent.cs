﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Network.Play;
using MineCase.World;

namespace MineCase.Server.Game.Entities.Components
{
    internal class SyncMobStateComponent : Component<EntityGrain>, IHandle<SpawnMob>
    {
        public SyncMobStateComponent(string name = "syncPlayerState")
            : base(name)
        {
        }

        protected override void OnAttached()
        {
            if (AttachedObject.GetValue(IsEnabledComponent.IsEnabledProperty))
                InstallPropertyChangedHandlers();
        }

        Task IHandle<SpawnMob>.Handle(SpawnMob message)
        {
            InstallPropertyChangedHandlers();
            return Task.CompletedTask;
        }

        private void InstallPropertyChangedHandlers()
        {
            AttachedObject.RegisterPropertyChangedHandler(EntityLookComponent.HeadYawProperty, OnHeadYawChanged);
            AttachedObject.RegisterPropertyChangedHandler(EntityLookComponent.YawProperty, OnYawChanged);
            AttachedObject.RegisterPropertyChangedHandler(EntityLookComponent.PitchProperty, OnPitchChanged);
            AttachedObject.RegisterPropertyChangedHandler(EntityWorldPositionComponent.EntityWorldPositionProperty, OnPositionChanged);
        }

        private void OnHeadYawChanged(object sender, PropertyChangedEventArgs<float> e)
        {
            uint eid = AttachedObject.GetValue(EntityIdComponent.EntityIdProperty);
            byte headyaw = (byte)(AttachedObject.GetValue(EntityLookComponent.HeadYawProperty) / 360 * 255);
            AttachedObject.QueueOperation(() => AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator().EntityHeadLook(eid, headyaw));
        }

        private void OnYawChanged(object sender, PropertyChangedEventArgs<float> e)
        {
            /*
            uint eid = AttachedObject.GetValue(EntityIdComponent.EntityIdProperty);
            byte yaw = (byte)(AttachedObject.GetValue(EntityLookComponent.YawProperty) / 360 * 255);
            byte pitch = (byte)(AttachedObject.GetValue(EntityLookComponent.PitchProperty) / 360 * 255);
            bool onGround = AttachedObject.GetValue(EntityOnGroundComponent.IsOnGroundProperty);

            // return AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator().EntityLook(eid, yaw, pitch, onGround);
            return AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator().EntityLookAndRelativeMove(eid, 0, 0, 0, yaw, pitch, onGround);
            */
        }

        private void OnPitchChanged(object sender, PropertyChangedEventArgs<float> e)
        {
            uint eid = AttachedObject.GetValue(EntityIdComponent.EntityIdProperty);
            byte yaw = (byte)(AttachedObject.GetValue(EntityLookComponent.YawProperty) / 360 * 255);
            byte pitch = (byte)(AttachedObject.GetValue(EntityLookComponent.PitchProperty) / 360 * 255);
            bool onGround = AttachedObject.GetValue(EntityOnGroundComponent.IsOnGroundProperty);

            // return AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator().EntityLook(eid, yaw, pitch, onGround);
            AttachedObject.QueueOperation(() => AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator().EntityLookAndRelativeMove(eid, 0, 0, 0, yaw, pitch, onGround));
        }

        private void OnPositionChanged(object sender, PropertyChangedEventArgs<EntityWorldPos> e)
        {
            uint eid = AttachedObject.GetValue(EntityIdComponent.EntityIdProperty);
            short x = (short)((e.NewValue.X - e.OldValue.X) * 32 * 128);
            short y = (short)((e.NewValue.Y - e.OldValue.Y) * 32 * 128);
            short z = (short)((e.NewValue.Z - e.OldValue.Z) * 32 * 128);
            byte yaw = (byte)(AttachedObject.GetValue(EntityLookComponent.YawProperty) / 360 * 255);
            byte pitch = (byte)(AttachedObject.GetValue(EntityLookComponent.PitchProperty) / 360 * 255);
            bool isOnGround = AttachedObject.GetValue(EntityOnGroundComponent.IsOnGroundProperty);

            AttachedObject.QueueOperation(() => AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator().EntityLookAndRelativeMove(eid, x, y, z, yaw, pitch, isOnGround));
        }
    }
}
