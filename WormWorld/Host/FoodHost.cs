using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using static System.Threading.Tasks.Task;

namespace WormWorld
{
    public class FoodHost: IHostedService
    {
        private WorldLogic _world;
        private FoodLogic _logic = new FoodLogic();

        public FoodHost(WorldLogic world)
        {
            _world = world;
        }

        private void RunAsync()
        {
            _world.DayFoodChanged += (source, state) =>
            {
                var ans = _logic.GetNewFood();
                while (!_world.AddFood(ans))
                {
                    ans = _logic.GetNewFood();
                }
                _world.ListOfFood.AddFood(ans);
                _world.WormDay();
            };
            _world.Start();
            var ans = _logic.GetNewFood();
            while (!_world.AddFood(ans))
            {
                ans = _logic.GetNewFood();
            }
            _world.ListOfFood.AddFood(ans);
            _world.WormDay();
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