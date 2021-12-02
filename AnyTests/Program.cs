using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AnyTests.ForUnitTests.BaseIn;

namespace AnyTests
{
    class Program
    {
        static void Main(string[] args)
        {

           
            
            using (DataBase.ApplicationContext db = new DataBase.ApplicationContext())
            {
                // создаем два объекта User
                Entity user1 = new Entity { id = 1, data = new ListDataGen().GenBehaviorStringData() };

                // добавляем их в бд
                db.Sets.Add(user1);
                bool cantSave = true;
                while (cantSave)
                {
                    try
                    {
                        db.SaveChanges();
                        cantSave = false;
                    }
                    catch (Exception e)
                    {
                        user1.id++;
                    }
                }
                Console.WriteLine("Объекты успешно сохранены");
 
                // получаем объекты из бд и выводим на консоль
                var users = db.Sets.ToList();
                Console.WriteLine("Список объектов:");
                foreach (Entity u in users)
                {
                    Console.WriteLine($"{u.id}.{u.data} ");
                }
            }
            /*
            var a = new SetBehavior(new SqlBase());
            a.GenBehavior(1);
            a.End();
            Console.WriteLine("here");
            
            var b = new TakeBehavior(new SqlBase());
            b.BehaviorForNum(4);
            b.End();
            */
        }
    }
}