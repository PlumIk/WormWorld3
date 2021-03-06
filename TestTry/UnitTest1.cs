using System;
using System.Collections.Generic;
using System.Linq;
using AnyTests;
using AnyTests.ForUnitTests.BaseIn;
using Microsoft.EntityFrameworkCore;
using WormWorld;
using WormWorld.Examples;
using Xunit;
using Xunit.Abstractions;

namespace TestTry
{
    public class UnitTest1
    {
        
        [Theory]
        [InlineData( new int[2] { 0,-1 } ) ]
        public void to_empty(int[] coord)
        { 
            var world = new WorldLogic();
            world.NextName = "Sirus";
            world.Start();
            bool ans=world.DoStep(coord);
            Assert.True(ans);



        }
        
        [Theory]
        [InlineData( new int[2] { 0,-1 } ) ]
        public void to_worm(int[] coord)
        { 
            var world = new WorldLogic();
            world.NextName = "Sirus";
            world.Start();
            world.NextName = "Drox";
            var newWorms = world.ListOfWorm.GetList();
            newWorms.Add(new WormExample(0,-1,world.GenName()));
            world.ListOfWorm.AccList(newWorms);
            bool ans=world.DoStep(coord);
            Assert.False(ans);
        }
        
        [Theory]
        [InlineData( new int[2] { 0,-1 } ) ]
        public void to_food(int[] coord)
        { 
            var world = new WorldLogic();
            world.NextName = "Sirus";
            world.Start();
            world.ListOfFood.AddFood(new int[2] { 0,-1 });
            bool ans=world.DoStep(coord);
            var newWorms = new List<WormExample>();
            foreach (var one in world.ListOfWorm.GetList())
            {
                one.AcStep(coord);
                newWorms.Add(one);
            }
            world.ListOfWorm.AccList(newWorms);
            Assert.True(ans);
            world.EndDay();
            
            foreach (var one in world.ListOfWorm.GetList())
            {
                Assert.Equal(20,one.Life);
            }
        }
        
        
        [Theory]
        [InlineData( new int[2] { 0,-1 } ) ]
        public void to_more_not_ok(int[] coord)
        { 
            var world = new WorldLogic();
            world.NextName = "Sirus";
            world.Start();
            bool ans=world.DoStep(coord);
            Assert.True(ans);
        }
        
        [Theory]
        [InlineData( new int[2] { 0,-1 } ) ]
        public void to_more_ok(int[] coord)
        { 
            var world = new WorldLogic();
            world.NextName = "Sirus";
            world.Start();
            world.NextName = "Drox";
            var newWorm = world.ListOfWorm.GetList();
            if(world.DoStep(coord))
                newWorm.Add(new WormExample(coord[0],coord[1],world.GenName()));
            world.ListOfWorm.AccList(newWorm);
            Assert.Equal(2,world.ListOfWorm.GetList().Count);
        }
        
        [Theory]
        [InlineData( 100) ]
        public void unik_name(int num)
        {
            var namer = new NameLogic();
            var names = new List<String>();
            for (int i = 0; i < num; i++)
            {
                names.Add(namer.GetName());
            }
            bool unik = true;
            
            for (int i = 0; i < names.Count; i++)
            {
                for (int j = i + 1; j < names.Count; j++)
                {
                    if (names[i] == names[j])
                        unik = false;
                }
            }
            
            Assert.True(unik);
        }
        
        [Theory]
        [InlineData( 4) ]
        public void unik_food(int num)
        {
            var world = new WorldLogic();
            var foodGen = new FoodLogic();
            world.NextName = "Sirus";
            world.Start();
            
            List<(int,int)> foodList=new ListDataGen().GenBehaviorListData();

            bool unik = true;
            //foodList.Add(new FoodExample(foodList[0].X, foodList[0].Y));
            for (int i = 0; i < foodList.Count; i++)
            {
                int j = i - 1;
                while (j > 0 && j > i-10)
                {
                    if (foodList[i].Item1 == foodList[j].Item1&&foodList[i].Item2 == foodList[j].Item2)
                        unik = false;
                    j--;
                }
                
            }
            
            Assert.True(unik);
        }
        
        [Theory]
        [InlineData( new int[2] { 0,0 }) ]
        public void food_in_worm(int[] coord)
        {
            var world = new WorldLogic();
            world.ListOfFood.AddFood(coord);;
            world.NextName = "Sirus";
            world.Start();

            world.AddFood();
            
            foreach (var one in world.ListOfWorm.GetList())
            {
                Assert.Equal(20, one.Life);
            }
        }
        
        [Theory]
        [InlineData( new int[2] { 3,3 }) ]
        public void worm_to_food(int[] coord)
        {
            var world = new WorldLogic();
            
            world.NextName = "Sirus";
            world.Start();
            int[] ans;

            var log = new WormLogic();
            while (world.ListOfFood.GetList().Count!=0)
            {
                var newWorm = new List<WormExample>();
                foreach (var one in world.ListOfWorm.GetList())
                {
                    ans = log.DoMyStep(world.ListOfFood.GetList(), world.ListOfWorm.GetList(), one);
                    if(world.DoStep(ans))
                        one.AcStep(ans);
                    world.ListOfWorm.AccList(newWorm);
                    Assert.True(one.Life<10);
                }
                world.EndDay();
            }

            bool eat = true;
            foreach (var one in world.ListOfWorm.GetList())
            {
                if (one.Life < 10)
                {
                    eat = false;
                }
            }
            
            Assert.True(eat);
        }
        
        
        [Theory]
        [InlineData( 1 ) ]
        public void WorldTry(int uname)
        {
            bool Ok = true;
            try
            {

                var _foodList =
                    "-5,4^-4,5^-3,1^-2,6^11,-5^4,1^-3,5^2,-8^1,-2^3,1^-3,-4^7,3^-2,0^-10,0^-1,0^-3,2^1,-3^-3,-3^7,-8^6,-1^13,3^-14,-4^9,1^-3,-10^6,5^-4,-6^6,-11^-2,0^-2,-1^0,-5^2,-4^4,-1^-8,5^-3,1^-2,-3^-5,-5^2,6^-4,0^-8,-10^-3,-5^-7,1^-2,11^3,5^1,3^5,-2^0,-7^1,5^3,-3^-1,2^11,-3^1,1^-3,-8^1,0^-1,3^3,5^-6,12^-3,1^0,0^-5,6^-9,-1^1,5^5,-2^1,1^5,0^4,4^2,6^-4,-4^5,-8^1,6^-3,5^0,8^2,-4^-5,1^8,-5^-1,-7^-2,5^1,-8^-5,5^2,-2^4,-2^13,-4^-2,-4^3,0^-4,-1^1,-5^6,-2^-5,1^-7,-2^2,-1^2,-6^-8,0^3,6^-3,-2^14,5^3,-3^14,-6^-3,3^0,2^-9,-3^3,3";
                var world = new WorldLogic();
                var logger = new FileHost(world, "test");
                var fooder = new FoodHost(world, _foodList);
                var namer = new NameHost(world);
                var wormer = new WormHost(world);
                world.Start();
                world.NextName = "Sirus";
                while (world.NowDay < 99)
                {
                    namer.MyWork();
                    fooder.MyWork();
                    wormer.MyWork();
                    logger.MyWork();
                    world.NowDay++;
                }

                logger.End();
            }
            catch (Exception)
            {
                Ok = false;
            }

            Assert.True(Ok);
        }
        
        
    }
}