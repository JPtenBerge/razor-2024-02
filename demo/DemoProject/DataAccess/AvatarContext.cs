using DemoProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DataAccess;

public class AvatarContext : DbContext
{
    public DbSet<Character> Characters { get; set; }
    
    public DbSet<Nation> Nations { get; set; }

    public AvatarContext(DbContextOptions<AvatarContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.ApplyConfiguration(new CharacterConfiguration());
        
        modelBuilder.Entity<Character>()
            .Property(x => x.Name)
            .HasMaxLength(50);
        
        modelBuilder.Entity<Character>()
            .Property(x => x.PhotoUrl)
            .HasMaxLength(4096);
        
        modelBuilder.Entity<Character>()
            .Property(x => x.Elements)
            .HasMaxLength(50);
    }
}