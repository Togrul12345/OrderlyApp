using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orderly.Domain.Entities;
using System;

namespace Orderly.Infrastructure.Persistence.Configurations
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name).IsRequired().HasMaxLength(150);
            builder.Property(r => r.Address).IsRequired().HasMaxLength(250);
            builder.Property(r => r.PhoneNumber).IsRequired().HasMaxLength(20);

     
            builder.HasMany(r => r.Categories)
                   .WithMany(c => c.Restaurants)
                   .UsingEntity<RestaurantCategory>(
                       l => l.HasOne(rc => rc.Category)
                             .WithMany()
                             .HasForeignKey(rc => rc.CategoryId)
                             .OnDelete(DeleteBehavior.Cascade),
                       r => r.HasOne(rc => rc.Restaurant)
                             .WithMany()
                             .HasForeignKey(rc => rc.RestaurantId)
                             .OnDelete(DeleteBehavior.Cascade),
                       j =>
                       {
                           j.HasKey(rc => new { rc.RestaurantId, rc.CategoryId });

                           j.HasData(
                               new RestaurantCategory { RestaurantId = 1, CategoryId = 1 }, 
                               new RestaurantCategory { RestaurantId = 1, CategoryId = 3 }, 
                               new RestaurantCategory { RestaurantId = 2, CategoryId = 2 },
                               new RestaurantCategory { RestaurantId = 3, CategoryId = 3 } 
                           );
                       }
                   );

            builder.HasMany(r => r.Products)
                   .WithOne(p => p.Restaurant)
                   .HasForeignKey(p => p.RestaurantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Orders)
                   .WithOne(o => o.Restaurant)
                   .HasForeignKey(o => o.RestaurantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Restaurant
                {
                    Id = 1,
                    Name = "Mangal Steak House",
                    Address = "Nizami küç. 45, Bakı",
                    PhoneNumber = "+994125554433",
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Restaurant
                {
                    Id = 2,
                    Name = "Sehrli Təndir",
                    Address = "İçərişəhər, Böyük Qala küç. 12, Bakı",
                    PhoneNumber = "+994124921122",
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Restaurant
                {
                    Id = 3,
                    Name = "Burger and More",
                    Address = "Fəvvarələr Meydanı, Bakı",
                    PhoneNumber = "+994125987654",
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}