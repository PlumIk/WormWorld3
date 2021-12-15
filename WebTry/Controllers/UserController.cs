using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebTry.Controllers
{

    [ApiController]
    [Route("api/{wormName}/getAction")]
    public class UsersController : ControllerBase
    {

        [HttpGet]
        public WormsEx Get([FromRoute] string wormName)
        {
            var a = new WormsEx();
            a.name = wormName;
            a.lifeStrength = 1000 - 7;
            a.position = new PositionEx(0,0);
            return  a;
        }
        
 
        // POST api/users
        [HttpPost]
        public InfoFromServerEx Post([FromRoute] string wormName, [FromBody] InfoForServerEx infoForServer)
        {
            List<WormExample> worms = new List<WormExample>();
            List<FoodExample> food = new List<FoodExample>();
            WormExample me=null;
            
            
            foreach (var one in infoForServer.worms)
            {
                WormExample toAdd = new WormExample(one.position.x, one.position.y, one.name, one.lifeStrength);
                if (wormName == toAdd.Name)
                {
                    me = toAdd;
                }
                worms.Add(toAdd);
            }
            
            foreach (var one in infoForServer.food)
            {
                FoodExample toAdd = new FoodExample(one.position.x, one.position.y,one.expiresin);
                food.Add(toAdd);
            }

            if (me is null)
            {
                return null;
            }
            var acc = new WormLogic().DoMyStep(food, worms, me);
            var a = new ActionEx();
            a.split = acc.Length == 3;

            if (me.X != acc[0])
            {
                a.direction = acc[0] > me.X ? "Right" : "Left";
            }
            else
            {
                a.direction = acc[1] > me.Y ? "Up" : "Down";
            }
            return new InfoFromServerEx(a);
        }
        
        
    }

}