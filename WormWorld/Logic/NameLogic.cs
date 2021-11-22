using System;
using System.Text;

namespace WormWorld
{
    public class NameLogic
    {
        private StringBuilder ExampleName;
        Random _rnd ;

        public NameLogic()
        {
            ExampleName = new StringBuilder("a1");
            _rnd = new Random();
        }
        
        public string GetName()
        {
            bool end = false;
            int step = 0;
            while (!end)
            {
                if (step < ExampleName.Length)
                {
                    if (ExampleName[step] >= '0' && ExampleName[step] < '0' + 9)
                    {
                        ExampleName[step] = (char) (ExampleName[step] + 1);
                        end = true;
                    }
                    else if (ExampleName[step] >= 'a' && ExampleName[step] < 'a' + 25)
                    {
                        ExampleName[step] = (char) (ExampleName[step] + 1);
                        end = true;
                    }
                    else
                    {
                        step++;
                    }
                }
                else
                {
                    step = _rnd.Next() % 2;
                    if (step == 0)
                    {
                        ExampleName.Append('a');
                    }
                    else
                    {
                        ExampleName.Append('1');
                    }

                    end = true;
                }
            }

            return ExampleName.ToString();
        }
    }
}