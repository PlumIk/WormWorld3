using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using static System.Threading.Tasks.Task;

namespace WormWorld
{
    public class NameHost
    {
        private NameLogic _logic = new();
        public WorldLogic _world;
        
        public NameHost()
        {
        }

        public void MyWork()
        {
            _world.NextName = _logic.GetName();
        }

        public void StartWork()
        {
            _world.NextName= _logic.GetName();
            _world.NewWorm += (_, _) =>
            {
                MyWork();
            };
            
        }
    }
}