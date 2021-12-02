using System;
using System.Text;

namespace WormWorld
{
    public class NameLogic
    {
        private StringBuilder _exampleName;
        Random _rnd ;

        public NameLogic()
        {
            _exampleName = new StringBuilder("a1");
            _rnd = new Random();
        }
        
        public string GetName()
        {
            bool end = false;
            int step = 0;
            while (!end)
            {
                if (step < _exampleName.Length)
                {
                    if (_exampleName[step] >= '0' && _exampleName[step] < '0' + 9)
                    {
                        _exampleName[step] = (char) (_exampleName[step] + 1);
                        end = true;
                    }
                    else if (_exampleName[step] >= 'a' && _exampleName[step] < 'a' + 25)
                    {
                        _exampleName[step] = (char) (_exampleName[step] + 1);
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
                    _exampleName.Append(step == 0 ? 'a' : '1');

                    end = true;
                }
            }

            return _exampleName.ToString();
        }
    }
}