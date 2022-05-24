using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amovie.Configuration
{
    public class MovieConfig : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");


            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Rating)
                .IsRequired();

            builder.Property(x => x.Duration)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Budget)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
