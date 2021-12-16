using Microsoft.EntityFrameworkCore;

namespace AnyTests.ForUnitTests.BaseIn
{
    public class DataBase
    {
        public sealed class ApplicationContext : DbContext
        {
            public DbSet<Entity> FoodList { get; set; }
         
            public ApplicationContext(DbContextOptions options) : base(options)
            {
                Database.EnsureCreated();
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if(!optionsBuilder.IsConfigured)
                    optionsBuilder.UseSqlServer("Server=localhost;Database=ForWorms;User Id=SA;Password=Sasha353!;");
            }
        }
    }
}