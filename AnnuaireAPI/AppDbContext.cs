﻿
using Microsoft.EntityFrameworkCore;
using AnnuaireModel;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace AnnuaireAPI.Controllers
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
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
                new Employee(  1,"Bill","Valence", "big.valence@gmail.com", "0632154879", "0125487963",1,1),
                new Employee(2, "coucou", "test", "test@test.fr", "0214569852","0214569852",1,2)
                );
            modelBuilder.Entity<Service>().HasData(
                new Service(1, "RH"),
                new Service(2, "Compta")
            );
            modelBuilder.Entity<Site>().HasData(
                new Site(1, "Lyon"),
                new Site(2, "Nantes")
            );
            
            modelBuilder.Entity<Employee>()
                .HasOne(s => s.Service)
                .WithMany()
                .HasForeignKey(s => s.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(s => s.Site)
                .WithMany()
                .HasForeignKey(s => s.SiteId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }
        

    }
}