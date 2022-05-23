using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataSeed
{
    public static class Seed
    {
        public static void SeedActors(this ModelBuilder builder)
        {
            builder.Entity<Actor>().HasData(new List<Actor>()
            {
                new Actor() { Id = 1, FirstName = "John", LastName = "Wick" },
                new Actor() { Id = 2, FirstName = "Tom", LastName = "Holland" },
                new Actor() { Id = 3, FirstName = "Tony", LastName = "Stark" },
                new Actor() { Id = 4, FirstName = "Mark", LastName = "Cruise" },
                new Actor() { Id = 5, FirstName = "Stephan", LastName = "Muller" }
            });
        }

        public static void SeedGenres(this ModelBuilder builder)
        {
            builder.Entity<Genre>().HasData(new List<Genre>()
            {
                new Genre(){Id = 1, Name = "Comedy"},
                new Genre(){Id = 2, Name = "Horror"},
                new Genre(){Id = 3, Name = "Drama"},
                new Genre(){Id = 4, Name = "Action"},
                new Genre(){Id = 5, Name = "Fantasy"},
            });
        }

        public static void SeedAuthors(this ModelBuilder builder)
        {
            builder.Entity<Author>().HasData(new List<Author>()
            {
                new Author(){Id = 1, FirstName = "Mike", LastName = "Fredrick"},
                new Author(){Id = 2, FirstName = "Terry", LastName = "Markus"},
                new Author(){Id = 3, FirstName = "Luckas", LastName = "Francis"},
                new Author() { Id = 4, FirstName = "Stephan", LastName = "Holh" },
                new Author() { Id = 5, FirstName = "Mark", LastName = "Cruise" },
            });
        }

        public static void SeedUsers(this ModelBuilder builder)
        {
            builder.Entity<User>().HasData(new List<User>()
            {
                new User() { Id = 20, Name = "John", Email="john@email.com", Password="admin", UserRole="admin" },
                new User() { Id = 21, Name = "Tom", Email="tom@email.com", Password="asdasd", UserRole="user" },
                new User() { Id = 22, Name = "Tony", Email="tony@email.com", Password="asdasd", UserRole="user" },
            });
        }
    }
}