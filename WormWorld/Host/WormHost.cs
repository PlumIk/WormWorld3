using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using WormWorld.Examples;
using static System.Threading.Tasks.Task;

namespace WormWorld
{
    public class WormHost: IHostedService
    {
        private WorldLogic _world;
        private INetworkService _networkService = new NetworkServiceFactory().GetNetworkService("http://localhost:5001/api","localhost",5001);
        private WormLogic _logic = new();

        public WormHost(WorldLogic world)
        {
            _world = world;
        }

        private void DoSteps()
        {
            var worms = _world.ListOfWorm.GetList();
            var newWorms =new List<WormExample>();
            foreach (var one in worms)
            {
                InfoForServerEx infoForServer = new ListsToInfo().Get(_world.ListOfFood.GetList(),_world.ListOfWorm.GetList());
                var response = _networkService.GetWormAction(one.Name, infoForServer);
                var infoFromServer = response.Result;
                int[] acc = new int[]{one.X,one.Y};
                if (infoFromServer.action.direction == "Up")
                {
                    acc[1]++;
                }else if (infoFromServer.action.direction == "Down")
                {
                    acc[1]--;
                }else if (infoFromServer.action.direction == "Right")
                {
                    acc[0]++;
                }else if (infoFromServer.action.direction == "Left")
                {
                    acc[0]--;
                }

                if (infoFromServer.action.split)
                {
                    one.Life -= 10;
                }
                else
                {
                    one.Life--;
                }

                if (_world.DoStep(acc))
                {
                    if (infoFromServer.action.split)
                    {
                        newWorms.Add(new WormExample(acc[0],acc[1],_world.GenName()));
                    }
                    else
                    {
                        one.AcStep(acc);
                    }
                }
                
                _world.ListOfWorm.AccList(worms);
            }

            foreach (var one in newWorms)
            {
                worms.Add(one);
            }
            
            _world.ListOfWorm.AccList(worms);
        }

        public void MyWork()
        {
            DoSteps();
            _world.EndDay();
        }
        private void RunAsync()
        {
            _world.DayWormChanged += (_, _) =>
            {
                MyWork();
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