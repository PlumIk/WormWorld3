using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using static System.Threading.Tasks.Task;

namespace WormWorld
{
    public class NameHost: IHostedService
    {
        private NameLogic _logic = new NameLogic();
        private readonly WorldLogic _world;
        
        public NameHost(WorldLogic world)
        {
            _world = world;
            _world.NextName= _logic.GetName();
        }

        private void RunAsync()
        {
            _world.NewWorm += (source, state) =>
            {
                _world.NextName = _logic.GetName();
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