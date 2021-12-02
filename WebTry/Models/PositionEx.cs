namespace WebTry.Models
{
    public class PositionEx
    {
        public int x { set; get; }
        public int y { set; get; }

        public PositionEx()
        {
            
        }

        public PositionEx(int inx, int iny)
        {
            x = inx;
            y = iny;
        }
    }
}