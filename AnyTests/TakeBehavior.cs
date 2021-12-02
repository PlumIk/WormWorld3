using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AnyTests
{
    public class TakeBehavior
    {
        private SqlBase _sql;

        public TakeBehavior(SqlBase sql)
        {
            _sql = sql;
            _sql.SetConnect();
        }

        public List<(int, int)> BehaviorForNum(int num)
        {
            List<(int, int)> ret = new List<(int, int)>();
            string command = "SELECT data FROM FoodList WHERE id=" + num;
            SqlDataReader reader = _sql.ExecuteCommand(command);
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    var coordsData = reader.GetValue(0);
                    string[] values = coordsData.ToString().Split('^');
                    foreach (var one in values)
                    {
                        string[] XY = one.Split(',');
                        ret.Add((Convert.ToInt32(XY[0]), Convert.ToInt32(XY[1])));
                    }

                }
            }

            foreach (var one in ret)
            {
                Console.WriteLine(one.Item1+"+"+one.Item2+"="+ (one.Item1+one.Item2));
            }
            return null;
        }

        public void End()
        {
            _sql.EndConnect();
        }
    }
}