using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using static System.Threading.Tasks.Task;

namespace WormWorld
{
    public class ForEnd: IHostedService
    {
        private readonly WorldLogic _world;
        private IHostApplicationLifetime _appLifetime;
        
        public ForEnd(WorldLogic world, IHostApplicationLifetime appLifetime)
        {
            _world = world;
            _appLifetime = appLifetime;
        }

        private void RunAsync()
        {
            _world.EndProg += (_,_) =>
            {
                _appLifetime.StopApplication();
            };
            
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Run(RunAsync, cancellationToken);
            return CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return CompletedTask;
        }
    }
}