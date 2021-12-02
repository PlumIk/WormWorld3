using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AnyTests;
using AnyTests.ForUnitTests.BaseIn;
using Microsoft.Extensions.Hosting;
using static System.Threading.Tasks.Task;

namespace WormWorld
{
    public class FoodHost: IHostedService
    {
        private WorldLogic _world;
        private List<(int, int)> _foodList;
        private int _logNum = 4;

        public FoodHost(WorldLogic world)
        {
            _foodList = new ListDataGen().GenBehaviorListData();
            new SetBehavior(new SqlBase()).GenBehavior(_foodList);
            _world = world;
        }

        public FoodHost(WorldLogic world, List<(int, int)> foodList)
        {
            _foodList = foodList;
            _world = world;
        }

        public void MyWork()
        {
            var ans = new[] {_foodList[_logNum].Item1,_foodList[_logNum].Item2};
            _logNum++;
            _world.ListOfFood.AddFood(ans);
            _world.AddFood();
            _world.WormDay();
        }
        private void RunAsync()
        {
            _logNum = 0;
            _world.DayFoodChanged += (_,_) =>
            {
               MyWork();
            };
            _world.Start();
            MyWork();
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