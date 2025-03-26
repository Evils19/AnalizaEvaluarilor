using AnalizaEvaluarilor.Domain.Entity.User;
using Microsoft.EntityFrameworkCore;

namespace AnalizaEvaluarilor.Infrastructure;

public class ApplicationDbContext : DbContext

{
    public ApplicationDbContext(DbContextOptions options) : base(options )
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
    
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var Role = new List<Role>()
        {
          new Role(){Id = Guid.NewGuid(),Name = "Admin"},
          new Role(){Id = Guid.NewGuid(),Name = "User"},
        };
        modelBuilder.Entity<Role>().HasData(Role);

        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.IdUser, ur.IdRole });  // Setează cheia primară compusă
        
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)    // UserRole are un User
            .WithMany(u => u.UserRoles) // User are multe UserRoles
            .HasForeignKey(ur => ur.IdUser); // Cheia străină
        
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)    // UserRole are un Role
            .WithMany(r => r.UserRoles) // Role are multe UserRoles
            .HasForeignKey(ur => ur.IdRole); // Cheia străină
    }
}