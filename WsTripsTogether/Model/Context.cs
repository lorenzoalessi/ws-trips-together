using Microsoft.EntityFrameworkCore;

namespace WsTripsTogether.Model;

public class Context : DbContext
{
    public Context()
    {
    }
    
    public Context(DbContextOptions options) : base(options)
    {
    }
    
    public virtual DbSet<User> Users { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(x => x.Username)
            .IsUnique();
        
        base.OnModelCreating(modelBuilder);
    }
}