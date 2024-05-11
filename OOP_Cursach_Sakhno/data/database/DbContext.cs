using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OOP_Cursach_Sakhno.data.models;

namespace OOP_Cursach_Sakhno.data.database
{
    public class DatabaseContext : DbContext
    {
        private static readonly DatabaseContext instance = new DatabaseContext();
        public static DatabaseContext Current => instance;

        public DbSet<Flat> Flats => Set<Flat>();
             public DbSet<Habitant> Habitant => Set<Habitant>();
             public DbSet<HabitantInFlat> HabitantList => Set<HabitantInFlat>();

             private DatabaseContext() : base()
             {
            Database.EnsureCreated();
             }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                
            var config = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("db.json")
                                .Build();

                optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
            }
    }
}
