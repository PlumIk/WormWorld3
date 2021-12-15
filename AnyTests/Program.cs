using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;
using AnyTests.ForUnitTests.BaseIn;

namespace AnyTests
{
    class Program
    {
        public static void Main(string[] args)
        {
            new worker().dod();
            
             /*
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            {
                // local dev, just approve all certs
                return errors == SslPolicyErrors.None ;
            };
            string url = "https://localhost:5001/api/John/getAction/";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";
            httpRequest.ContentType = "application/json";
            using (var httpResponse = httpRequest.GetResponse())
            using (var responseStream = httpResponse.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                string response = reader.ReadToEnd();
                Console.WriteLine(response);
            }
    
*/

            /*
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
            */
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