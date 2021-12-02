namespace WormWorld.Examples
{
    public class FoodExample
    
    {
        private int[] _coord = new int[2] ;

        public int X => _coord[0];

        public int Y => _coord[1];

        public int Life { get; private set; } = 10;

        public void Tick()
        {
            Life--;
        }

        public bool IsAlive()
        {
            return Life >= 0;
        }
        public FoodExample(int inx,int iny)
        {
            _coord[0]= inx;
            _coord[1] = iny;
        }

        private FoodExample(int inx,int iny, int life)
        {
            _coord[0]= inx;
            _coord[1] = iny;
            Life = life;
        }


        public FoodExample Copy()
        {
            var ret = new FoodExample(X, Y, Life);
            return ret;
        }
        
        
    }
}