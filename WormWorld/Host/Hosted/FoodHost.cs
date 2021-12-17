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
    public class FoodHost{
        public WorldLogic _world;
        private List<(int, int)> _foodList;
        private int _logNum = 0;

        public FoodHost(DataBase.ApplicationContext db)
        {
            _foodList = new ListDataGen().GenBehaviorListData();
            
            Entity data = new Entity { id = 1, data = new ListDataGen().GenBehaviorStringData(_foodList)};
            db.FoodList.Add(data);
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
                    data.id++;
                }
            }

        }
        
        public FoodHost(DataBase.ApplicationContext db, int num)
        {
            
            var users = db.FoodList.ToList();
            _foodList = new ListDataGen().GenBehaviorListData(users[num - 1].data);

        }
        public void MyWork()
        {
            var ans = new[] {_foodList[_logNum].Item1,_foodList[_logNum].Item2};
            _logNum++;
            _world.ListOfFood.AddFood(ans);
            _world.AddFood();
            _world.WormDay();
        }
        public void StartWork()
        {
            _logNum = 0;
            _world.DayFoodChanged += (_,_) =>
            {
               MyWork();
            };
            _world.Start();
            MyWork();
        }
        
    }

   
}