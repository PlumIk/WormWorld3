using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebTry.Models;

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
        public InfoForServer Post([FromRoute] string wormName, [FromBody] InfoFromServerEx infoForServer)
        {
            Console.WriteLine( infoForServer.food.Length);
            Console.WriteLine( infoForServer.worms[0].name);
            var a = new ActionEx();
            a.direction = "Up";
            a.split = false;
            return new InfoForServer(a);
        }
        
        
    }

}