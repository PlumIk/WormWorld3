using System;
using System.Collections.Generic;
using WormWorld;

namespace AnyTests.ForUnitTests.BaseIn
{
    public class ListDataGen
    {
        
        public List<(int,int)> GenBehaviorListData()
        {
            FoodLogic FoodGen = new FoodLogic();
            List<(int, int)> food = new List<(int, int)>();
            while (food.Count != 100)
            {
                int nowi = food.Count - 1;
                bool CanAdd = true;
                var newCoord = FoodGen.GetNewFood();
                while (nowi >= food.Count - 10 && nowi >= 0 && CanAdd)
                {
                    if (newCoord[0] == food[nowi].Item1 && newCoord[1] == food[nowi].Item2)
                    {
                        CanAdd = false;
                    }

                    nowi--;
                }

                if (CanAdd)
                {
                    food.Add((newCoord[0], newCoord[1]));
                }
            }

            return food;
        }
        public string GenBehaviorStringData()
        {
            
            List<(int, int)> food = GenBehaviorListData();
            

            string command = "";
            for (int i = 0; i < food.Count; i++)
            {
                if (i != 0)
                {
                    command += "^";
                }

                command += food[i].Item1 + "," + food[i].Item2;
            }
            
            return command;
        }
        
        public string GenBehaviorStringData( List<(int, int)> food)
        {
            
            string command = "";
            for (int i = 0; i < food.Count; i++)
            {
                if (i != 0)
                {
                    command += "^";
                }

                command += food[i].Item1 + "," + food[i].Item2;
            }
            
            return command;
        }
        
        public List<(int,int)> GenBehaviorListData( string food)
        {
            
            List<(int, int)> foodL = new List<(int, int)>();
            string[] values =food.Split('^');
            foreach (var one in values)
            {
                string[] XY = one.Split(',');
                foodL.Add((Convert.ToInt32(XY[0]), Convert.ToInt32(XY[1])));
            }

            return foodL;
        }
    }
}