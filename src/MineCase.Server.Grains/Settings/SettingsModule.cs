using Autofac;

namespace MineCase.Server.Settings
{
    internal class SettingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServerSettingsGrain>();
        }
    }
}
