using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Client.Game.Entities.Components;

namespace MineCase.Client.Game.Entities
{
    public class PlayerEntity : Entity
    {
        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            SetComponent(new PlayerInputTranslatorComponent());
            SetComponent(new PlayerMotorComponent());
        }
    }
}
