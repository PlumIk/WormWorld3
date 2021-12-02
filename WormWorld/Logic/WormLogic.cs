using System;
using System.Collections.Generic;
using WormWorld.Examples;

namespace WormWorld
{
    public class WormLogic
    {

        public int[] DoMyStep(List<FoodExample> foodAround, List<WormExample> worms, WormExample me)
        {
            int[] ret = {me.X, me.Y};
            var canPlace = new bool[] {true, true, true, true};
            var point = new int[2];
            foreach (var one in worms)
            {
                int i = 0;
                if (one.X == (me.X + 1) && one.Y == me.Y)
                {
                    canPlace[i] = false;
                }

                if (one.X == (me.X - 1) && one.Y == me.Y)
                {
                    canPlace[i] = false;
                }

                if (one.X == me.X && one.Y == me.Y + 1)
                {
                    canPlace[i] = false;
                }

                if (one.X == me.X + 1 && one.Y == me.Y - 1)
                {
                    canPlace[i] = false;
                }
            }

            if (canPlace[0] || canPlace[1] || canPlace[2] || canPlace[3])
            {
                if (me.Life >= 20)
                {
                    ret = new[] {me.X, me.Y, 0};
                    if (canPlace[0])
                    {
                        ret[0] += 1;
                    }
                    else if (canPlace[1])
                    {
                        ret[0] -= 1;
                    }
                    else if (canPlace[2])
                    {
                        ret[1] += 1;
                    }
                    else if (canPlace[3])
                    {
                        ret[1] -= 1;
                    }

                    me.OneMore();
                    return ret;

                }
                
                var nowPath = me.Life + 1;
                foreach (var foodEx in foodAround)
                {

                    var path = Math.Abs(me.X - foodEx.X) + Math.Abs(me.Y - foodEx.Y);
                    if (path <= me.Life && path <= foodEx.Life && path < nowPath)
                    {
                        point[0] = foodEx.X;
                        point[1] = foodEx.Y;
                        nowPath = path;
                    }
                }

                if (me.X != point[0] || me.Y != point[1])
                {
                    var find = false;
                    if (me.X != point[0])
                    {
                        if (point[0] > me.X && canPlace[0])
                        {
                            ret = new[] {me.X + 1, me.Y};
                            find = true;
                        }
                        else if (canPlace[1])
                        {
                            ret = new[] {me.X - 1, me.Y};
                            find = true;
                        }
                    }

                    if (me.Y != point[1] && !find)
                    {
                        if (point[1] > me.Y && canPlace[2])
                        {
                            ret = new[] {me.X, me.Y + 1};
                        }
                        else if (canPlace[3])
                        {
                            ret = new[] {me.X, me.Y - 1};
                        }
                    }
                }
            }
            me.Life--;
           

            return ret;
        }
    }
}