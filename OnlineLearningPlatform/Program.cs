using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Entities;
using OnlineLearningPlatform.Enums;

namespace OnlineLearningPlatform
{
    internal class Program
    {
        static void SeedStudentsAndEnrollments()
        {
            using var context = new AppDbContext();

            if (!context.Students.Any())
            {
                var students = new List<Student>
                {
                    new Student
                    {
                        FullName = "Ali Hassan",
                        Email = "ali@gmail.com",
                        DateOfBirth = new DateTime(2003, 5, 10)
                    },
                    new Student
                    {
                        FullName = "Mona Ahmed",
                        Email = "mona@gmail.com",
                        DateOfBirth = new DateTime(2002, 8, 15)
                    },
                    new Student
                    {
                        FullName = "Youssef Samir",
                        Email = "youssef@gmail.com",
                        DateOfBirth = new DateTime(2004, 2, 20)
                    },
                    new Student
                    {
                        FullName = "Nour Mohamed",
                        Email = "nour@gmail.com",
                        DateOfBirth = new DateTime(2003, 11, 5)
                    },
                    new Student
                    {
                        FullName = "Malak Omar",
                        Email = "malak@gmail.com",
                        DateOfBirth = new DateTime(2002, 6, 12)
                    }
                };

                context.Students.AddRange(students);
                context.SaveChanges();

                Console.WriteLine("Students seeded successfully.");
            }

            if (!context.Enrollments.Any())
            {
                var students = context.Students
                    .OrderBy(s => s.Id)
                    .Take(5)
                    .ToList();

                if (students.Count < 5)
                {
                    Console.WriteLine("Not enough students.");
                    return;
                }

                var enrollments = new List<Enrollment>
                {
                    new Enrollment
                    {
                        StudentId = students[0].Id,
                        CourseId = 1,
                        EnrollmentDate = DateTime.Now,
                        Progress = 100,
                        Status = EnrollmentStatus.Completed,
                        FinalGrade = 95
                    },
                    new Enrollment
                    {
                        StudentId = students[0].Id,
                        CourseId = 2,
                        EnrollmentDate = DateTime.Now,
                        Progress = 40,
                        Status = EnrollmentStatus.Active
                    },
                    new Enrollment
                    {
                        StudentId = students[1].Id,
                        CourseId = 1,
                        EnrollmentDate = DateTime.Now,
                        Progress = 60,
                        Status = EnrollmentStatus.Active
                    },
                    new Enrollment
                    {
                        StudentId = students[1].Id,
                        CourseId = 3,
                        EnrollmentDate = DateTime.Now,
                        Progress = 100,
                        Status = EnrollmentStatus.Completed,
                        FinalGrade = 88
                    },
                    new Enrollment
                    {
                        StudentId = students[2].Id,
                        CourseId = 2,
                        EnrollmentDate = DateTime.Now,
                        Progress = 20,
                        Status = EnrollmentStatus.Dropped
                    },
                    new Enrollment
                    {
                        StudentId = students[2].Id,
                        CourseId = 5,
                        EnrollmentDate = DateTime.Now,
                        Progress = 100,
                        Status = EnrollmentStatus.Completed,
                        FinalGrade = 92
                    },
                    new Enrollment
                    {
                        StudentId = students[3].Id,
                        CourseId = 3,
                        EnrollmentDate = DateTime.Now,
                        Progress = 30,
                        Status = EnrollmentStatus.Active
                    },
                    new Enrollment
                    {
                        StudentId = students[4].Id,
                        CourseId = 1,
                        EnrollmentDate = DateTime.Now,
                        Progress = 0,
                        Status = EnrollmentStatus.Dropped
                    }
                };

                context.Enrollments.AddRange(enrollments);
                context.SaveChanges();

                Console.WriteLine("Enrollments seeded successfully.");

            }

        }
        static void AddInstructor()
        {
            using var context = new AppDbContext();

            var instructor = new Instructor
            {
                FullName = "Heba Mahmoud",
                Email = "heba@gmail.com",
                Bio = "Software Engineer",
                JoinDate = DateTime.Now
            };

            context.Instructors.Add(instructor);
            context.SaveChanges();

            Console.WriteLine(
                $"Instructor added successfully. ID: {instructor.Id}");
        }

        static void AddCourse()
        {
            using var context = new AppDbContext();

            int instructorId = 1;
            int categoryId = 3;

            var instructorExists = context.Instructors
                .Any(i => i.Id == instructorId);

            var categoryExists = context.Categories
                .Any(c => c.Id == categoryId);

            if (!instructorExists)
            {
                Console.WriteLine("Instructor not found.");
                return;
            }

            if (!categoryExists)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            var course = new Course
            {
                Title = "Entity Framework Core",
                Description = "Learn EF Core",
                Price = 900,
                Level = CourseLevel.Intermediate,
                IsPublished = false,
                PublishedDate = null,
                InstructorId = instructorId,
                CategoryId = categoryId
            };

            context.Courses.Add(course);
            context.SaveChanges();

            Console.WriteLine($"Course added. ID: {course.Id}");
        }

        static void AddSeveralStudents()
        {
            using var context = new AppDbContext();

            var students = new List<Student>
        {
            new Student
            {
                FullName = "Sara Mohamed",
                Email = "sara2@gmail.com",
                DateOfBirth = new DateTime(2003, 1, 10)
            },
            new Student
            {
                FullName = "Omar Ali",
                Email = "omar2@gmail.com",
                DateOfBirth = new DateTime(2002, 4, 15)
            },
            new Student
            {
                FullName = "Laila Hassan",
                Email = "laila@gmail.com",
                DateOfBirth = new DateTime(2004, 7, 20)
            }
        };

                context.Students.AddRange(students);

                context.SaveChanges();

                foreach (var student in students)
                {
                    Console.WriteLine(
                        $"{student.FullName} - ID: {student.Id}");
                }
            }

        static void EnrollStudent()
        {
            using var context = new AppDbContext();

            int studentId = 1;
            int courseId = 3;

            var studentExists = context.Students
                .Any(s => s.Id == studentId);

            if (!studentExists)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            var course = context.Courses
                .FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            if (!course.IsPublished)
            {
                Console.WriteLine("Enrollment refused: Course is not published.");
                return;
            }

            var alreadyEnrolled = context.Enrollments
                .Any(e => e.StudentId == studentId &&
                          e.CourseId == courseId);

            if (alreadyEnrolled)
            {
                Console.WriteLine("Student is already enrolled.");
                return;
            }

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.Now,
                Progress = 0,
                Status = EnrollmentStatus.Active,
                FinalGrade = null
            };

            context.Enrollments.Add(enrollment);
            context.SaveChanges();

            Console.WriteLine("Student enrolled successfully.");
        }

        static void FindCourse()
        {
            using var context = new AppDbContext();

            int courseId = 1;

            var courseById = context.Courses.Find(courseId);

            if (courseById == null)
                Console.WriteLine("Course not found.");
            else
                Console.WriteLine($"Found by ID: {courseById.Title}");

            string title = "SQL Server";

            var courseByTitle = context.Courses
                .FirstOrDefault(c => c.Title == title);

            if (courseByTitle == null)
                Console.WriteLine("Course not found.");
            else
                Console.WriteLine($"Found by Title: {courseByTitle.Title}");
        }

        static void FilterCourses()
        {
            using var context = new AppDbContext();

            decimal minPrice = 400;
            decimal maxPrice = 1200;

            var courses = context.Courses
                .Where(c => c.Price >= minPrice &&
                            c.Price <= maxPrice)
                .OrderByDescending(c => c.Price)
                .Select(c => new
                {
                    c.Title,
                    c.Level,
                    c.Price
                })
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine(
                    $"{course.Title} | {course.Level} | {course.Price}");
            }
        }

        static void PrintCategories()
        {
            using var context = new AppDbContext();

            int categoryId = 1;

            var subCategories = context.Categories
                .Where(c => c.ParentCategoryId == categoryId)
                .ToList();

            Console.WriteLine("Sub-Categories:");

            foreach (var category in subCategories)
            {
                Console.WriteLine(category.Name);
            }

            var topCategories = context.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToList();

            Console.WriteLine("Top-Level Categories:");

            foreach (var category in topCategories)
            {
                Console.WriteLine(category.Name);
            }
        }

        static void PrintCourseRoster()
        {
            using var context = new AppDbContext();

            int courseId = 1;

            var studentIds = context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.StudentId)
                .ToList();

            Console.WriteLine(
                $"Number of Enrolled Students: {studentIds.Count}");

            var students = context.Students
                .Where(s => studentIds.Contains(s.Id))
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine(student.FullName);
            }
        }

        static void ChangeCoursePrice()
        {
            using var context = new AppDbContext();

            int courseId = 1;
            decimal newPrice = 800;

            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            course.Price = newPrice;

            context.SaveChanges();

            Console.WriteLine("Course price updated successfully.");
        }

        static void UpdateProgress()
        {
            using var context = new AppDbContext();

            int studentId = 1;
            int courseId = 2;
            decimal newProgress = 100;

            if (newProgress < 0 || newProgress > 100)
            {
                Console.WriteLine("Progress must be between 0 and 100.");
                return;
            }

            var enrollment = context.Enrollments.Find(
                studentId, courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            enrollment.Progress = newProgress;

            if (newProgress == 100)
            {
                enrollment.Status = EnrollmentStatus.Completed;
            }

            context.SaveChanges();

            Console.WriteLine("Progress updated successfully.");
        }

        static void PublishCourse()
        {
            using var context = new AppDbContext();

            int courseId = 4;

            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            course.IsPublished = true;
            course.PublishedDate = DateTime.Now;

            context.SaveChanges();

            Console.WriteLine("Course published successfully.");
        }

        static void UnenrollStudent()
        {
            using var context = new AppDbContext();

            int studentId = 5;
            int courseId = 1;

            var enrollment = context.Enrollments.Find(
                studentId, courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            context.Enrollments.Remove(enrollment);

            context.SaveChanges();

            Console.WriteLine("Student unenrolled successfully.");
        }

        static void TryDeleteCategory()
        {
            using var context = new AppDbContext();

            int categoryId = 1;

            var category = context.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            context.Categories.Remove(category);

            try
            {
                context.SaveChanges();

                Console.WriteLine("Category deleted.");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(
                    "Cannot delete category because it has related records.");

                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
            }
        }

        static void SafeDeleteCategory()
        {
            using var context = new AppDbContext();

            int categoryId = 1;

            var category = context.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            bool hasSubCategories = context.Categories
                .Any(c => c.ParentCategoryId == categoryId);

            if (hasSubCategories)
            {
                Console.WriteLine(
                    "Cannot delete category because it has sub-categories.");

                return;
            }

            context.Categories.Remove(category);
            context.SaveChanges();

            Console.WriteLine("Category deleted successfully.");
        }

        static void DeleteCourse()
        {
            using var context = new AppDbContext();

            int courseId = 1;

            var course = context.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            int beforeCount = context.Enrollments
                .Count(e => e.CourseId == courseId);

            Console.WriteLine(
                $"Enrollments before deletion: {beforeCount}");

            context.Courses.Remove(course);

            context.SaveChanges();

            int afterCount = context.Enrollments
                .Count(e => e.CourseId == courseId);

            Console.WriteLine(
                $"Enrollments after deletion: {afterCount}");
        }

        static void BulkPriceRaise()
        {
            using var context = new AppDbContext();

            var affectedRows = context.Courses
                .Where(c => c.IsPublished &&
                            c.Level == CourseLevel.Beginner)
                .ExecuteUpdate(s =>
                    s.SetProperty(
                        c => c.Price,
                        c => c.Price * 1.10m
                    )
                );

            Console.WriteLine(
                $"Affected Courses: {affectedRows}");
        }

        static void BulkDelete()
        {
            using var context = new AppDbContext();

            var affectedRows = context.Enrollments
                .Where(e => e.Status == EnrollmentStatus.Dropped)
                .ExecuteDelete();

            Console.WriteLine(
                $"Deleted Enrollments: {affectedRows}");
        }


        static void Main(string[] args)
        {
            SeedStudentsAndEnrollments();

            Console.WriteLine("\n========== CREATE ==========");

            AddInstructor();
            AddCourse();
            AddSeveralStudents();
            EnrollStudent();

            Console.WriteLine("\n========== READ ==========");

            FindCourse();
            FilterCourses();
            PrintCategories();
            PrintCourseRoster();

            Console.WriteLine("\n========== UPDATE ==========");

            ChangeCoursePrice();
            UpdateProgress();
            PublishCourse();

            Console.WriteLine("\n========== DELETE ==========");

            UnenrollStudent();
            TryDeleteCategory();
            SafeDeleteCategory();
            DeleteCourse();

            Console.WriteLine("\n========== BULK OPERATIONS ==========");

            BulkPriceRaise();
            BulkDelete();

            Console.WriteLine("\nAll Operations Completed.");
        }

    }
}
