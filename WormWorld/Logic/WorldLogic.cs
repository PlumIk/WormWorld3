using System;
using System.Linq;
using System.Threading;

namespace WormWorld
{
    public class WorldLogic
    {
        public int NowDay = 0;

        public FoodList ListOfFood = new();
        public WormList ListOfWorm;
        public string NextName;

        public event EventHandler<int> DayFoodChanged;
        public event EventHandler<int> DayWormChanged;
        public event EventHandler<int> NewWorm;
        public event EventHandler<int> DayEnd;
        public event EventHandler<int> EndProg;

        public void Start()
        {
            ListOfWorm = new WormList(NextName);
            
        }

        public bool DoStep(int[] wormCoord)
        {
            var worms = ListOfWorm.GetList();
            return worms.All(one => one.X != wormCoord[0] || one.Y != wormCoord[1]);
        }
        public void AddFood()
        {
            TryToEat();
        }
        public void StartDay()
        {
            if (NowDay < 99)
            {
                NowDay++;
                DayFoodChanged?.Invoke(this, NowDay);

            }
            else
            {
                Thread.Sleep(1000);
                EndThis();
            }
        }

        public void WormDay()
        {
            TryToEat();
            DayWormChanged?.Invoke(this, NowDay);
        }

        private void TryToEat()
        {
            ListOfFood.Acc(ListOfWorm.Eat(ListOfFood.GetList()));
        }
        public void EndDay()
        {
            TryToEat();
            ListOfFood.IsDie();
            ListOfWorm.IsDie();
            DayEnd?.Invoke(this,NowDay);
        } 

        public string GenName()
        {
            NewWorm?.Invoke(this,NowDay);
            return NextName;
        }

        private void EndThis()
        {
            EndProg?.Invoke(this, NowDay);
        }
    }
}