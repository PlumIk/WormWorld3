using System;
using System.Threading;
using WormWorld.CastomConteners;

namespace WormWorld
{
    public class WorldLogic
    {
        public int NowDay = 0;

        public FoodList ListOfFood = new FoodList();
        public WormList ListOfWorm;
        public String NextName;

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
            foreach (var one in worms)
            {
                if (one.X == wormCoord[0] && one.Y == wormCoord[1])
                {
                    return false;
                }
            }
            return true;
        }
        public bool AddFood(int[] foodCoord)
        {
            var worms = ListOfWorm.GetList();
            foreach (var one in worms)
            {
                if (one.X == foodCoord[0] && one.Y == foodCoord[1])
                {
                    one.Eat();
                    ListOfWorm.AccList(worms);
                    return false;
                }
            }

            return true;
        }
        public void StartDay()
        {
            if (NowDay < 100)
            {
                NowDay++;
                if (DayFoodChanged != null)
                {
                    DayFoodChanged(this, NowDay);
                }

            }
            else
            {
                Thread.Sleep(1000);
                end_this();
            }
        }

        public void WormDay()
        {
            if (DayWormChanged != null)
            {
                DayWormChanged(this, NowDay);
            }
        }
        public void EndDay()
        {
            ListOfFood.Acc(ListOfWorm.Eat(ListOfFood.GetList()));
            ListOfFood.IsDie();
            ListOfWorm.IsDie();
            if (DayEnd != null) 
                DayEnd(this,NowDay);
        }

        public String GenName()
        {
            if (NewWorm != null) 
                NewWorm(this,NowDay);
            return NextName;
        }

        private void end_this()
        {
            if (EndProg != null)
            {
                EndProg(this, NowDay);
            }
        }
    }
}