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
        private string Name = "/home/alex/Prog/Reshotka/WormWorld3/WormWorld/";
        //private const string Name = "D:/Prog/Reshotka/WormWorld3/Solution1/WormWorld/out.txt";

        
        public FileHost(WorldLogic world, string also)
        {
            Name += also + "out.txt";
            if (!File.Exists(Name))
            {
                File.Create(Name);
            }
            File.WriteAllText(Name, "Start:\n");
            _world = world;
        }
        public FileHost(WorldLogic world)
        {
            Name += "out.txt";
            if (!File.Exists(Name))
            {
                File.Create(Name);
            }
            File.WriteAllText(Name, "Start:\n");
            _world = world;
        }

        public void MyWork()
        {
            File.AppendAllText(Name, "Day "+(_world.NowDay+1)+":"+_world.ListOfWorm.Info()+_world.ListOfFood.Info()+"\n");
            _world.StartDay();
        }

        public void End()
        {
            File.AppendAllText(Name, "Total Worms = " + _world.ListOfWorm.GetList().Count);
        }

        private void RunAsync()
        {
            _world.DayEnd += (_, _) =>
            {
                MyWork();
            };
            _world.EndProg += (_, _) =>
            {
                End();
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