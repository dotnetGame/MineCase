using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Commands;

namespace MineCase.Server.Commands.Vanilla
{
    public class GameModeCommand
        : SimpleCommand
    {
        public GameModeCommand()
            : base("gamemode", "Changes the player to a specific game mode")
        {
        }

        public override async Task<bool> Execute(ICommandSender commandSender, IReadOnlyList<ICommandArgument> args)
        {
            var player = (IPlayer)commandSender;

            if (args.Count < 1)
            {
                throw new CommandWrongUsageException(this, "Invalid arguments");
            }

            GameMode.Class gameModeClass;

            switch (((UnresolvedArgument)args[0]).RawContent)
            {
                case "survival":
                case "s":
                case "0":
                    gameModeClass = GameMode.Class.Survival;
                    break;
                case "creative":
                case "c":
                case "1":
                    gameModeClass = GameMode.Class.Creative;
                    break;
                case "adventure":
                case "ad":
                case "2":
                    gameModeClass = GameMode.Class.Adventure;
                    break;
                case "spectator":
                case "sp":
                case "3":
                    gameModeClass = GameMode.Class.Spectator;
                    break;
                default:
                    throw new CommandWrongUsageException(this);
            }

            var target = player;

            if (args.Count == 2)
            {
                var rawArg = (UnresolvedArgument)args[1];

                if (rawArg is TargetSelectorArgument targetSelector)
                {
                    switch (targetSelector.Type)
                    {
                        case TargetSelectorType.NearestPlayer:
                            break;
                        case TargetSelectorType.RandomPlayer:
                            break;
                        case TargetSelectorType.AllPlayers:
                            break;
                        case TargetSelectorType.AllEntites:
                            break;
                        case TargetSelectorType.Executor:
                            break;
                        default:
                            throw new CommandException(this);
                    }

                    throw new CommandException(this, "Sorry, this feature has not been implemented.");
                }
                else
                {
                    var user = await player.GetUser();
                    var session = await user.GetGameSession();

                    target = await (await session.FindUserByName(rawArg.RawContent)).GetPlayer();

                    if (target == null)
                    {
                        throw new CommandException(this, $"Player \"{rawArg.RawContent}\" not found, may be offline or not existing.");
                    }
                }
            }

            var targetDesc = await target.GetDescription();
            var mode = targetDesc.GameMode;
            mode.ModeClass = gameModeClass;
            targetDesc.GameMode = mode;

            return true;
        }
    }
}
