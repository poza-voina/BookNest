using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Domain.Entities;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Publisher> Publishers => Set<Publisher>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Author> Authors => Set<Author>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>();
        modelBuilder.Entity<Book>();
        modelBuilder.Entity<Category>();
        modelBuilder.Entity<Comment>();
        modelBuilder.Entity<Publisher>();
        modelBuilder.Entity<Review>();
        modelBuilder.Entity<Author>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}