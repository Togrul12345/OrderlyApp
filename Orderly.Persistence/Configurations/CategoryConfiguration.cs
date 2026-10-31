using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orderly.Domain.Entities;
using System;

namespace Orderly.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(500);

            builder.HasMany(c => c.Restaurants)
                   .WithMany(r => r.Categories)
                   .UsingEntity<RestaurantCategory>(
                       l => l.HasOne(rc => rc.Restaurant)
                             .WithMany()
                             .HasForeignKey(rc => rc.RestaurantId)
                             .OnDelete(DeleteBehavior.Cascade),
                       r => r.HasOne(rc => rc.Category)
                             .WithMany()
                             .HasForeignKey(rc => rc.CategoryId)
                             .OnDelete(DeleteBehavior.Cascade),
                       j =>
                       {
                           j.HasKey(rc => new { rc.RestaurantId, rc.CategoryId });

                    
                          
                       }
                   );

            builder.HasMany(c => c.Products)
                   .WithOne(p => p.Category)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

    
            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Steaklər",
                    Description = "Ən dadlı və şirəli ət yeməkləri",
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 2,
                    Name = "Milli Yeməklər",
                    Description = "Odun peçində və təndirdə bişən təamlar",
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Category
                {
                    Id = 3,
                    Name = "Fast Food & Burgerlər",
                    Description = "Dadlı burgerlər və kartof qızartmaları",
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}