using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AnyTests;
using AnyTests.ForUnitTests.BaseIn;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static System.Threading.Tasks.Task;

namespace WormWorld
{
    public class FoodHost: IHostedService
    {
        private WorldLogic _world;
        private List<(int, int)> _foodList;
        private int _logNum = 0;

        public FoodHost(WorldLogic world)
        {
            _foodList = new ListDataGen().GenBehaviorListData();
            
            var opt = new DbContextOptionsBuilder<DataBase.ApplicationContext>().Options;
            using (DataBase.ApplicationContext db = new DataBase.ApplicationContext(opt))
            {

                Entity user1 = new Entity { id = 1, data = new ListDataGen().GenBehaviorStringData(_foodList)};

                // добавляем их в бд
                db.FoodList.Add(user1);
                bool cantSave = true;
                while (cantSave)
                {
                    try
                    {
                        db.SaveChanges();
                        cantSave = false;
                    }
                    catch (Exception e)
                    {
                        user1.id++;
                    }
                }
            }
            _world = world;

        }

        public FoodHost(WorldLogic world, DataBase.ApplicationContext db)
        {
            List<(int, int)> food = new List<(int, int)>();
            var users = db.FoodList.ToList();
            string[] values = users[0].data.ToString().Split('^');
            foreach (var one in values)
            {
                string[] XY = one.Split(',');
                food.Add((Convert.ToInt32(XY[0]), Convert.ToInt32(XY[1])));
            }
            _foodList = food;
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