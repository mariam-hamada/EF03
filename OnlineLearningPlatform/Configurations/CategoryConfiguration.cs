using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.Configurations
{
    public class CategoryConfiguration :
         IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(c => c.Description)
                    .HasMaxLength(500);

            builder.HasOne(c => c.ParentCategory)
                    .WithMany(c => c.SubCategories)
                    .HasForeignKey(c => c.ParentCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Programming",
                    Description = "Programming Courses",
                    ParentCategoryId = null
                },
                new Category
                {
                    Id = 2,
                    Name = "Backend",
                    Description = "Backend Development",
                    ParentCategoryId = 1
                },
                new Category
                {
                    Id = 3,
                    Name = "ASP.NET",
                    Description = "ASP.NET Development",
                    ParentCategoryId = 2
                },
                new Category
                {
                    Id = 4,
                    Name = "Frontend",
                    Description = "Frontend Development",
                    ParentCategoryId = 1
                },
                new Category
                {
                    Id = 5,
                    Name = "Database",
                    Description = "Database Courses",
                    ParentCategoryId = null
                }
            );
        }
    }
}
