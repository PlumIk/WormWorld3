using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using static System.Threading.Tasks.Task;


namespace WormWorld
{
    public class FileHost 
    {
        public WorldLogic _world;
        private string Name = "/home/alex/Prog/Reshotka/WormWorld3/WormWorld/";
        //private const string Name = "D:/Prog/Reshotka/WormWorld3/Solution1/WormWorld/out.txt";

        
        public FileHost(string also)
        {
            Name += also + "out.txt";
            if (!File.Exists(Name))
            {
                File.Create(Name);
            }
            File.WriteAllText(Name, "Start:\n");
        }
        public FileHost()
        {
            Name += "out.txt";
            if (!File.Exists(Name))
            {
                File.Create(Name);
            }
            File.WriteAllText(Name, "Start:\n");
        }

        public void MyWork()
        {
            File.AppendAllText(Name, "Day "+(_world.NowDay+1)+":"+_world.ListOfWorm.Info()+_world.ListOfFood.Info()+"\n");
            _world.StartDay();
        }

        public void End()
        {
            File.AppendAllText(Name, "Total Worms = " + _world.ListOfWorm.GetList().Count);
        }

        public void StartWork()
        {
            _world.DayEnd += (_, _) =>
            {
                MyWork();
            };
            _world.EndProg += (_, _) =>
            {
                End();
            };
        }
        
    }
}