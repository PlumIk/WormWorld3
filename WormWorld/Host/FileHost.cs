using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using static System.Threading.Tasks.Task;


namespace WormWorld
{
    public class FileHost : IHostedService
    {
        private readonly WorldLogic _world;
        private const string Name = "D:/Prog/Reshotka/WormWorld3/Solution1/WormWorld/out.txt";

        public FileHost(WorldLogic world)
        {
           
            if (!File.Exists(Name))
            {
                File.Create(Name);
            }
            File.WriteAllText(Name, "Start:\n");
            _world = world;
        }

        private void RunAsync()
        {
            _world.DayEnd += (source, state) =>
            {
                File.AppendAllText(Name, _world.ListOfWorm.Info()+_world.ListOfFood.Info()+"\n");
                _world.StartDay();
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