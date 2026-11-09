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
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(i => i.Email)
                    .IsRequired()
                    .HasMaxLength(150);

            builder.Property(i => i.Bio)
                    .HasMaxLength(500);

            builder.Property(i => i.JoinDate)
                    .IsRequired();

            builder.HasData(
                new Instructor
                {
                    Id = 1,
                    FullName = "Ahmed Mohamed",
                    Email = "ahmed@gmail.com",
                    Bio = "C# Instructor",
                    JoinDate = new DateTime(2024, 1, 1)
                },
                new Instructor
                {
                    Id = 2,
                    FullName = "Sara Ali",
                    Email = "sara@gmail.com",
                    Bio = "Database Instructor",
                    JoinDate = new DateTime(2024, 2, 1)
                },
                new Instructor
                {
                    Id = 3,
                    FullName = "Omar Hassan",
                    Email = "omar@gmail.com",
                    Bio = null,
                    JoinDate = new DateTime(2024, 3, 1)
                }
            );
        }
    }
}
