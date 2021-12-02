namespace WebTry.Models
{
    public class InfoForServer
    {
        public ActionEx action { set; get; }

        public InfoForServer(ActionEx inac)
        {
            action = inac;
        }
    }
}