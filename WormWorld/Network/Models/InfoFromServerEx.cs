namespace WormWorld
{
    public class InfoFromServerEx
    {
        public ActionEx action { set; get; }
        
        public InfoFromServerEx(){}
        public InfoFromServerEx(ActionEx inac)
        {
            action = inac;
        }
    }
}