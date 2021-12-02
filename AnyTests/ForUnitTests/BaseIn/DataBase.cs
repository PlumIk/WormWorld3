using Microsoft.EntityFrameworkCore;

namespace AnyTests.ForUnitTests.BaseIn
{
    public class DataBase
    {
        public sealed class ApplicationContext : DbContext
        {
            public DbSet<Entity> Sets { get; set; }
         
            public ApplicationContext()
            {
                Database.EnsureCreated();
            }
 
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=ForWorms;User Id=SA;Password=Sasha353!;");
            }
        }
    }
}