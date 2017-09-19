using Orleans.Runtime.Configuration;
using System;
using System.Threading;

namespace MineCase.Server
{
    class Program
    {
        private static readonly ManualResetEvent _exitEvent = new ManualResetEvent(false);

        private static OrleansHostWrapper hostWrapper;

        static int Main(string[] args)
        {
            int exitCode = StartSilo(args);

            Console.WriteLine("Press Ctrl+C to terminate...");
            Console.CancelKeyPress += (s, e) => _exitEvent.Set();
            _exitEvent.WaitOne();

            exitCode += ShutdownSilo();

            //either StartSilo or ShutdownSilo failed would result on a non-zero exit code. 
            return exitCode;
        }


        private static int StartSilo(string[] args)
        {
            // define the cluster configuration
            var config = new ClusterConfiguration();
            config.LoadFromFile("OrleansConfiguration.dev.xml");
            config.AddMemoryStorageProvider();
            config.UseStartupType<Startup>();

            hostWrapper = new OrleansHostWrapper(config, args);
            return hostWrapper.Run();
        }

        private static int ShutdownSilo()
        {
            if (hostWrapper != null)
            {
                return hostWrapper.Stop();
            }
            return 0;
        }

        // Workaroud for assembly references
        private void Dummy()
        {
            var type = new[]
            {
                typeof(Orleans.Storage.MemoryStorage)
            };
        }
    }
}