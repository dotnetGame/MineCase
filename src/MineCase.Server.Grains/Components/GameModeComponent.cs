using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Game;

namespace MineCase.Server.Components
{
    internal class GameModeComponent : Component
    {
        public static readonly DependencyProperty<GameMode> GameModeProperty =
            DependencyProperty.Register<GameMode>("GameMode", typeof(GameModeComponent));

        public GameMode GameMode => AttachedObject.GetValue(GameModeProperty);

        public GameModeComponent(string name = "gameMode")
            : base(name)
        {
        }

        public void SetGameMode(GameMode value) =>
            AttachedObject.SetLocalValue(GameModeProperty, value);
    }
}
