using System;

namespace WormWorld.Examples
{
    public class WormExample

    {
        private int[] _coord = new int[2];

        public int X
        {
            get => _coord[0];
            private set => _coord[0] = value;
        }

        public int Y
        {
            get => _coord[1];
            private set => _coord[1] = value;
        }

        public int Life { set; get; } = 10;

        public string Name { get; private set; }

        public WormExample(int x, int y, string inName)
        {
            X = x;
            Y = y;
            Name = inName;
        }

        private WormExample(int x, int y, string inName, int life)
        {
            X = x;
            Y = y;
            Life = life;
            Name = inName;
        }

        public WormExample (string inName)
        {
            Name = inName;
            X = 0;
            Y = 0;
        }

        void SetCoord(int a, int b)
        {
            X = a;
            Y = b;
        }

        public void OneMore()
        {
            Life -= 10;
        }

        public void Eat()
        {
            Life += 10;
        }


        public void AcStep(int[] ac)
        {
            SetCoord(ac[0], ac[1]);
        }

        public WormExample Copy()
        {
            var ret = new WormExample(X,Y,Name,Life);
            return ret;
        }
        
    }
}