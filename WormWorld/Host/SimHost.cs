using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace WormWorld
{
    public class SimHost: IHostedService
    {
        WorldLogic world;//некоторый сериализуемый формат данных
        NameHost nameGenerator;
        FileHost logger;
        FoodHost foodGenerator;
        WormHost logic;
        
        public SimHost(NameHost nameGenerator, FileHost logger, FoodHost foodGenerator, WormHost logic, WorldLogic _world,IHostApplicationLifetime _appLifetime) {
            this.nameGenerator = nameGenerator;
            this.logger = logger;
            this.foodGenerator = foodGenerator;
            this.logic = logic;
            world = _world;
            nameGenerator._world = world;
            logger._world = world;
            foodGenerator._world = world;
            logic._world = world;
            _world.EndProg += (_,_) =>
            {
                _appLifetime.StopApplication();
            };
        }


        private void StartWork()
        {
            nameGenerator.StartWork();
            logger.StartWork();
            logic.StartWork();
            foodGenerator.StartWork();
        }
        
        
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.StartWork();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}