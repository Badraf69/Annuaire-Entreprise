using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AnnuaireAPI;

public class AppDbContext : DbContext
{
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Services> Services { get; set; }
    public DbSet<Sites> Sites { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        OptionsBuilder.UseSqlServer("connection");
    }
    
}