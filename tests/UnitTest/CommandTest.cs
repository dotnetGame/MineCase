using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Game.Commands;
using Xunit;

namespace MineCase.UnitTest
{
    public class CommandTest
    {
        private class OutputCommandSender : ICommandSender
        {
            private readonly TextWriter _tw;

            public OutputCommandSender(TextWriter tw)
            {
                _tw = tw;
            }

            public Task<bool> HasPermission(Permission permission)
            {
                return Task.FromResult(true);
            }

            public Task SendMessage(string msg)
            {
                _tw.WriteLine(msg);
                return Task.CompletedTask;
            }
        }

        private class TestCommand : SimpleCommand
        {
            public TestCommand()
                : base("test")
            {
            }

            public override Task<bool> Execute(ICommandSender commandSender, IReadOnlyList<ICommandArgument> args)
            {
                commandSender.SendMessage(string.Join(", ", args.Select(arg => arg.ToString())));
                return Task.FromResult(true);
            }
        }

        [Fact]
        public void Test1()
        {
            var commandMap = new CommandMap();
            commandMap.RegisterCommand(new TestCommand());

            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                commandMap.Dispatch(new OutputCommandSender(sw), "/test 1 ~2 @p[arg1=233,arg2=]");
                var str = sb.ToString();
                Console.Write(str);
            }
        }
    }
}
