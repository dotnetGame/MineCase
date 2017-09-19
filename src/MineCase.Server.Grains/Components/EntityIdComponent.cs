using MineCase.Engine;
using MineCase.Server.Game.Entities.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Components
{
    internal class EntityIdComponent : Component, IHandle<SpawnEntity>
    {
        public static readonly DependencyProperty<uint> EntityIdProperty =
            DependencyProperty.Register<uint>("EntityId", typeof(EntityIdComponent));

        public uint EntityId => AttachedObject.GetValue(EntityIdProperty);

        public EntityIdComponent(string name = "entityId")
            : base(name)
        {
        }

        Task IHandle<SpawnEntity>.Handle(SpawnEntity message)
        {
            return AttachedObject.SetLocalValue(EntityIdProperty, message.EntityId);
        }
    }
}
