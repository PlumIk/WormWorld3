using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnyTests.ForUnitTests.BaseIn;
using WormWorld;

namespace AnyTests
{
    public class SetBehavior
    {
        private SqlBase _sql;

        public SetBehavior(SqlBase sql)
        {
            _sql = sql;
            _sql.SetConnect();
        }

        public void GenBehavior(int num)
        {
            string getId = "SELECT MAX(id) FROM FoodList";
            SqlDataReader reader = _sql.ExecuteCommand(getId);
            var lid=1;
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    lid = Convert.ToInt32( reader.GetValue(0));
                }
            }
            reader.Close();
            lid += 1;

            string command = "INSERT INTO FoodList VALUES(" + lid+",'" ;
            command += new ListDataGen().GenBehaviorStringData();

            command += "')";
            _sql.ExecuteCommand(command);
        }

        public void End()
        {
            _sql.EndConnect();
        }
    }
}