using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Game.Entities.Components
{
    internal class ExperienceComponent : Component
    {
        public static readonly DependencyProperty<uint> ExperienceProperty =
            DependencyProperty.Register<uint>("Experience", typeof(ExperienceComponent));

        private uint _levelMaxExp = 7;
        private uint _totalExp = 0;
        private uint _level = 0;

        public uint Experience => AttachedObject.GetValue(ExperienceProperty);

        public float ExperienceBar => (float)Experience / _levelMaxExp;

        public uint Level => _level;

        public uint TotalExperience => _totalExp;

        public ExperienceComponent(string name = "experience")
            : base(name)
        {
        }
    }
}
