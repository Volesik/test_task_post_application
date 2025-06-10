using Microsoft.EntityFrameworkCore;
using PostApp.DL.Abstraction.Extensions;
using PostApp.DL.EntityFramework.Models;

namespace PostApp.DL.EntityFramework;

public class PostAppContext : DbContext
{
    public PostAppContext() {}

    public PostAppContext(DbContextOptions<PostAppContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ConfigureBaseProperties();
        modelBuilder.Entity<Post>().ConfigureBaseProperties();
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
    
    public DbSet<User> Users { get; set; } = null!;
    
    public DbSet<Post> Posts { get; set; } = null!;
}