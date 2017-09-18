using MineCase.Engine;
using MineCase.Server.Network.Play;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Entities.Components
{
    internal class ExperienceComponent : Component, IHandle<PlayerLoggedIn>
    {
        public static readonly DependencyProperty<uint> ExperienceProperty =
            DependencyProperty.Register<uint>("Experience", typeof(ExperienceComponent));

        private uint _levelMaxExp = 7;
        private uint _totalExp = 0;
        private uint _level = 0;

        public ExperienceComponent(string name = "experience")
            : base(name)
        {
        }

        protected override Task OnAttached()
        {
            return base.OnAttached();
        }

        async Task IHandle<PlayerLoggedIn>.Handle(PlayerLoggedIn message)
        {
            var currentExp = AttachedObject.GetValue(ExperienceProperty);
            await AttachedObject.GetComponent<ClientboundPacketComponent>().GetGenerator()
                .SetExperience((float)currentExp / _levelMaxExp, _level, _totalExp);
        }
    }
}
