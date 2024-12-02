using System.Reflection;
using Microsoft.EntityFrameworkCore;
using StajTakipSistemi.Models;

namespace StajTakipSistemi.Database;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    // Db set
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }
}