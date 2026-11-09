using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.Entities;
using OnlineLearningPlatform.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.Configurations
{
    public class CourseConfiguration :
       IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                    .IsRequired()
                    .HasMaxLength(200);

            builder.Property(c => c.Description)
                    .HasMaxLength(1000);

            builder.Property(c => c.Price)
                    .HasPrecision(10, 2)
                    .IsRequired();

            builder.Property(c => c.Level)
                    .IsRequired();

            builder.Property(c => c.IsPublished)
                     .IsRequired();

            builder.Property(c => c.PublishedDate);

            
            builder.HasOne(c => c.Instructor)
                    .WithMany(i => i.Courses)
                    .HasForeignKey(c => c.InstructorId)
                    .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasOne(c => c.Category)
                    .WithMany(cat => cat.Courses)
                    .HasForeignKey(c => c.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Course
                {
                    Id = 1,
                    Title = "C# Fundamentals",
                    Description = "Learn C# Basics",
                    Price = 500,
                    Level = CourseLevel.Beginner,
                    IsPublished = true,
                    PublishedDate = new DateTime(2025, 1, 1),
                    InstructorId = 1,
                    CategoryId = 1
                },
                new Course
                {
                    Id = 2,
                    Title = "ASP.NET Core",
                    Description = "Web Development",
                    Price = 1000,
                    Level = CourseLevel.Intermediate,
                    IsPublished = true,
                    PublishedDate = new DateTime(2025, 2, 1),
                    InstructorId = 1,
                    CategoryId = 3
                },
                new Course
                {
                    Id = 3,
                    Title = "SQL Server",
                    Description = "Database Fundamentals",
                    Price = 700,
                    Level = CourseLevel.Beginner,
                    IsPublished = true,
                    PublishedDate = new DateTime(2025, 3, 1),
                    InstructorId = 2,
                    CategoryId = 5
                },
                new Course
                {
                    Id = 4,
                    Title = "Advanced SQL",
                    Description = "Advanced Database",
                    Price = 1200,
                    Level = CourseLevel.Advanced,
                    IsPublished = false,
                    PublishedDate = null,
                    InstructorId = 2,
                    CategoryId = 5
                },
                new Course
                {
                    Id = 5,
                    Title = "HTML and CSS",
                    Description = "Frontend Basics",
                    Price = 400,
                    Level = CourseLevel.Beginner,
                    IsPublished = true,
                    PublishedDate = new DateTime(2025, 4, 1),
                    InstructorId = 3,
                    CategoryId = 4
                },
                new Course
                {
                    Id = 6,
                    Title = "Advanced Backend",
                    Description = "Advanced Backend Development",
                    Price = 1500,
                    Level = CourseLevel.Advanced,
                    IsPublished = false,
                    PublishedDate = null,
                    InstructorId = 3,
                    CategoryId = 2
                }
            );
        }
    }
}
