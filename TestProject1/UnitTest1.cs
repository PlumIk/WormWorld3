using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WormWorld;
using WormWorld.Examples;
using Xunit;
using Xunit.Abstractions;

namespace TestProject1
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

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
        [InlineData( 100) ]
        public void unik_food(int num)
        {
            var world = new WorldLogic();
            var foodGen = new FoodLogic();
            world.NextName = "Sirus";
            world.Start();
            int[] ans;
            for (int i = 0; i < num; i++)
            {
                ans = foodGen.GetNewFood();
                while (!world.AddFood(ans))
                {
                    ans = foodGen.GetNewFood();
                }
            }
            bool unik = true;

            var foodList = world.ListOfFood.GetList();
            //foodList.Add(new FoodExample(foodList[0].X, foodList[0].Y));
            for (int i = 0; i < foodList.Count; i++)
            {
                for (int j = i + 1; j < foodList.Count; j++)
                {
                    if (foodList[i].X == foodList[j].X&&foodList[i].Y == foodList[j].Y)
                        unik = false;
                }
            }
            
            Assert.True(unik);
        }
        
        [Theory]
        [InlineData( new int[2] { 0,0 }) ]
        public void food_in_worm(int[] coord)
        {
            var world = new WorldLogic();
            
            world.NextName = "Sirus";
            world.Start();
            int[] ans;

            bool added = world.AddFood(coord);
            
            Assert.False(added);
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

            world.AddFood(coord);
            
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
        
        
        
    }
}