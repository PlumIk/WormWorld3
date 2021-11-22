using System;
using System.Collections.Generic;
using WormWorld.Examples;

namespace WormWorld
{
    public class WormLogic
    {
        
        private int[] _point = new int[2];

        public int[] DoMyStep(List<FoodExample> foodAround, List<WormExample> worms, WormExample me)
        {
            int[] ret = {me.X, me.Y};
            bool[] canplace = new bool[] {true, true, true, true};
            foreach (var one in worms)
            {
                int i = 0;
                if (one.X == (me.X + 1) && one.Y == me.Y)
                {
                    canplace[i] = false;
                }

                if (one.X == (me.X - 1) && one.Y == me.Y)
                {
                    canplace[i] = false;
                }

                if (one.X == me.X && one.Y == me.Y + 1)
                {
                    canplace[i] = false;
                }

                if (one.X == me.X + 1 && one.Y == me.Y - 1)
                {
                    canplace[i] = false;
                }
            }

            if (me.Life >= 14 && (new Random().Next()) % 5 <= 2)
            {

                if (canplace[0] || canplace[1] || canplace[2] || canplace[3])
                {
                    ret = new[] {me.X, me.Y, 0};
                    if (canplace[0])
                    {
                        ret[0] += 1;
                    }
                    else if (canplace[1])
                    {
                        ret[0] -= 1;
                    }
                    else if (canplace[2])
                    {
                        ret[1] += 1;
                    }
                    else if (canplace[3])
                    {
                        ret[1] -= 1;
                    }
                    me.OneMore();
                    return ret;
                }
            }
            
            me.Life--;
            int nowPath=me.Life +1;
            foreach (var FoodEx in foodAround)
            {

                var path = Math.Abs(me.X - FoodEx.X) + Math.Abs(me.Y - FoodEx.Y);
                if (path <= me.Life && path <= FoodEx.Life&&path<nowPath)
                {
                    _point[0] = FoodEx.X;
                    _point[1] = FoodEx.Y;
                    nowPath = path;
                }
            }
            
            if (me.X!=_point[0]||me.Y!=_point[1])
            {
                var find = false;
                if (me.X != _point[0])
                {
                    if (_point[0] > me.X&& canplace[0])
                    {
                        ret = new[] {me.X + 1, me.Y};
                        find = true;
                    }
                    else if(canplace[1])
                    {
                        ret = new[] {me.X - 1, me.Y};
                        find = true;
                    }
                }
                
                if (me.Y != _point[1]&&!find)
                {
                    if (_point[1] > me.Y&&canplace[2])
                    {
                        ret = new[] {me.X , me.Y+1};
                    }
                    else if(canplace[3])
                    {
                        ret = new[] {me.X , me.Y-1};
                    }
                }
            }

            return ret;
        }
    }
}