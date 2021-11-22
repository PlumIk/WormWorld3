using System;

namespace WormWorld
{
    public class FoodLogic
    {
        private Random _r;

        public FoodLogic()
        {
            _r = new Random();
        }

        public int[] GetNewFood()
        {
            var ret = new int[2];
            ret[0] = NextNormal();
            ret[1] = NextNormal();
            return ret;
        }
        
        public int NextNormal( double mu = 0, double sigma = 5)
        {

            var u1 = _r.NextDouble();

            var u2 = _r.NextDouble();

            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); 

            var randNormal = mu + sigma * randStdNormal;

            return (int)Math.Round(randNormal);

        }
    }
}