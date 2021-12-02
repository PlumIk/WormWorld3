using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WormWorld.Examples;

namespace WormWorld
{
    public class FoodList
    {
        private List<FoodExample> _foodCont;

        public FoodList()
        {
            _foodCont = new List<FoodExample>();
        }
        
        public List<FoodExample> GetList()
        {
            return _foodCont.Select(one => one.Copy()).ToList();
        }

        public void Acc(List<FoodExample> toAcc)
        {
            _foodCont = toAcc;
        }

        private void Tick()
        {
            foreach (var one in _foodCont)
            {
                one.Tick();
            }
        }
        public void IsDie()
        {
            Tick();
            var a = _foodCont.Where(worm => worm.Life <= 0).ToList();

            foreach (var worm in a)
            {
                _foodCont.Remove(worm);
            }
        }

        public void AddFood(int[] coord)
        {
            FoodExample one = new FoodExample(coord[0], coord[1]);
            _foodCont.Add(one);
        }

        public string Info()
        {
            StringBuilder ret = new StringBuilder("");
            ret.Append("Food:[");
            foreach (var food in _foodCont)
            {
                ret.Append("(" + food.X + "," + food.Y+ ","+food.Life +"),");
            }

            ret.Append("]");
            return ret.ToString();
        }
    }
}