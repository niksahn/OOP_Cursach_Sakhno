using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OOP_Cursach_Sakhno.data.models;

namespace OOP_Cursach_Sakhno.data.database
{
    public class DatabaseContext : DbContext
    {
             public DbSet<Flat> Flats => Set<Flat>();
             public DbSet<Habitant> Habitant => Set<Habitant>();
             public DbSet<HabitantInFlat> HabitantList => Set<HabitantInFlat>();

             public DatabaseContext() : base()
             {
                Database.EnsureCreated();
             }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                var config = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .Build();

                optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
            }
    }
}
