using DataSeed;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(DataContext)));
            modelBuilder.SeedActors();
            modelBuilder.SeedGenres();
            modelBuilder.SeedAuthors();
            modelBuilder.SeedUsers(); 
        }
    }
}
