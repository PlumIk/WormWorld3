using System.Collections.Generic;
using WormWorld.Examples;

namespace WormWorld
{
    public class ListsToInfo
    {
        public InfoForServerEx Get(List<FoodExample> food, List<WormExample> worms)
        {
            InfoForServerEx ret = new InfoForServerEx();
            WormsEx[] wormRet = new WormsEx[worms.Count];
            for (int i=0;i<worms.Count;i++)
            {
                var one = worms[i];
                var a = new WormsEx();
                a.name = one.Name;
                a.lifeStrength = one.Life;
                a.position = new PositionEx(one.X, one.Y);
                wormRet[i] = a;
            }

            FoodEx[] foodRet = new FoodEx [food.Count];
            for (int i=0;i<food.Count;i++)
            {
                var one = food[i];
                var a = new FoodEx();
                a.expiresin = one.Life;
                a.position = new PositionEx(one.X, one.Y);
                foodRet[i] = a;
            }

            ret.food = foodRet;
            ret.worms = wormRet;
            return ret;
        }
    }
}