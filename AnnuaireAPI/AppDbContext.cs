
using Microsoft.EntityFrameworkCore;
using AnnuaireModel;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace AnnuaireAPI.Controllers
{

    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Site> Sites { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=d:/DbTest.db");
            base.OnConfiguring(optionsBuilder);
        }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration de l'entité Employee

            modelBuilder.Entity<Employee>().HasData(
                new Employee(  1,"Bill","Valence", "big.valence@gmail.com", "0632154879", "0125487963",null,null),
                new Employee(2, "coucou", "test", "test@test.fr", "0214569852","0214569852",null,null)
                );

            base.OnModelCreating(modelBuilder);
        }

    }
}