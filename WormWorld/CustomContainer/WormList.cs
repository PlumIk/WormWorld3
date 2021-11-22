using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WormWorld.Examples;

namespace WormWorld.CastomConteners
{
    public class WormList
    {
        private List<WormExample> _wormsList = new List<WormExample>();


        public WormList(string name)
        {
            _wormsList.Add(new WormExample(name));
        }

        public List<WormExample> GetList()
        {
            return _wormsList.Select(one => one.Copy()).ToList();
        }

        public void AccList(List<WormExample> toAcc)
        {
            _wormsList = toAcc;
        }

        public void AddOne(WormExample worm)
        {
            _wormsList.Add(worm);
        }

        public void IsDie()
        {
            var a = _wormsList.Where(worm => worm.Life <= 0).ToList();

            foreach (var worm in a)
            {
                _wormsList.Remove(worm);
            }
        }

        public List<FoodExample> Eat(List<FoodExample> foodList)
        {
            var eaten = new List<FoodExample>();
            foreach (var one in _wormsList)
            {
                foreach (var two in foodList)
                {
                    if (one.X == two.X && one.Y == two.Y)
                    {
                        one.Eat();
                        eaten.Add(two);
                    }
                }
            }

            foreach (var one in eaten)
            {
                foodList.Remove(one);
            }

            return foodList;
        }
        public string Info()
        {
            var ret = new StringBuilder("");
            ret.Append("Worms:[");
            foreach (var worm in _wormsList)
            {
                ret.Append(worm.Name + "-" + worm.Life + "(" + worm.X + "," + worm.Y + "),");
            }

            ret.Append("]");
            return ret.ToString();
        }
    }
}