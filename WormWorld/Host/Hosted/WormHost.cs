using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using WormWorld.Examples;
using static System.Threading.Tasks.Task;

namespace WormWorld
{
    public class WormHost
    {
        public WorldLogic _world;
        private WormLogic _logic = new();

        public WormHost()
        {
           
        }

        private void DoSteps()
        {
            var worms = _world.ListOfWorm.GetList();
            var newWorms =new List<WormExample>();
            foreach (var one in worms)
            {
                var ans = _logic.DoMyStep(_world.ListOfFood.GetList(),
                    _world.ListOfWorm.GetList(), one);
                if (ans.Length == 2)
                {
                    if (_world.DoStep(ans))
                    {
                        one.AcStep(ans);
                    }
                }
                else
                {
                    if (_world.DoStep(ans))
                    {
                        newWorms.Add(new WormExample(ans[0], ans[1], _world.GenName()));
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
        public void StartWork()
        {
            _world.DayWormChanged += (_, _) =>
            {
                MyWork();
            };
        }

    }
}