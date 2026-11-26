using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Models;
using OnlineLearningPlatform.Models.Enums;

namespace OnlineLearningPlatform.Data;

public static class DataSeeder
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Instructor>().HasData(
            new Instructor { Id = 1, FullName = "Ahmed Hassan", Email = "ahmed.hassan@learn.com", Bio = "Senior .NET developer", JoinedAt = new DateTime(2023, 1, 15) },
            new Instructor { Id = 2, FullName = "Sara Mostafa", Email = "sara.mostafa@learn.com", Bio = null, JoinedAt = new DateTime(2023, 6, 1) },
            new Instructor { Id = 3, FullName = "Omar Khaled", Email = "omar.khaled@learn.com", Bio = "UI/UX designer", JoinedAt = new DateTime(2024, 2, 10) });

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Programming", Description = "All programming topics", ParentCategoryId = null },
            new Category { Id = 2, Name = "Backend", Description = null, ParentCategoryId = 1 },
            new Category { Id = 3, Name = "ASP.NET", Description = "ASP.NET Core", ParentCategoryId = 2 },
            new Category { Id = 4, Name = "Frontend", Description = null, ParentCategoryId = 1 },
            new Category { Id = 5, Name = "Design", Description = "UI / UX", ParentCategoryId = null },
            new Category { Id = 6, Name = "Miscellaneous", Description = "Empty category (used to test safe delete)", ParentCategoryId = null });

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Title = "ASP.NET Core Web API", Description = "Build REST APIs", Price = 49.99m, Level = CourseLevel.Intermediate, IsPublished = true, PublishedDate = new DateTime(2024, 1, 10), InstructorId = 1, CategoryId = 3 },
            new Course { Id = 2, Title = "C# Fundamentals", Description = "Learn C# from zero", Price = 29.99m, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2023, 9, 1), InstructorId = 1, CategoryId = 2 },
            new Course { Id = 3, Title = "React for Beginners", Description = null, Price = 39.99m, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2024, 3, 5), InstructorId = 2, CategoryId = 4 },
            new Course { Id = 4, Title = "Advanced EF Core", Description = "Deep dive into EF Core", Price = 79.99m, Level = CourseLevel.Advanced, IsPublished = false, PublishedDate = null, InstructorId = 1, CategoryId = 3 },
            new Course { Id = 5, Title = "UI Design Basics", Description = null, Price = 19.99m, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2024, 4, 20), InstructorId = 3, CategoryId = 5 },
            new Course { Id = 6, Title = "Microservices with .NET", Description = null, Price = 99.99m, Level = CourseLevel.Advanced, IsPublished = false, PublishedDate = null, InstructorId = 2, CategoryId = 2 });

        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, FullName = "Mona Ali", Email = "mona.ali@mail.com", DateOfBirth = new DateTime(2000, 5, 12) },
            new Student { Id = 2, FullName = "Youssef Adel", Email = "youssef.adel@mail.com", DateOfBirth = new DateTime(1999, 11, 30) },
            new Student { Id = 3, FullName = "Nour Ibrahim", Email = "nour.ibrahim@mail.com", DateOfBirth = new DateTime(2001, 3, 8) },
            new Student { Id = 4, FullName = "Karim Samir", Email = "karim.samir@mail.com", DateOfBirth = new DateTime(1998, 7, 21) });

        modelBuilder.Entity<Enrollment>().HasData(
            new Enrollment { StudentId = 1, CourseId = 1, EnrolledAt = new DateTime(2024, 2, 1), ProgressPercentage = 60, Status = EnrollmentStatus.Active, FinalGrade = null },
            new Enrollment { StudentId = 1, CourseId = 2, EnrolledAt = new DateTime(2024, 2, 5), ProgressPercentage = 100, Status = EnrollmentStatus.Completed, FinalGrade = 92.50m },
            new Enrollment { StudentId = 2, CourseId = 1, EnrolledAt = new DateTime(2024, 2, 10), ProgressPercentage = 30, Status = EnrollmentStatus.Active, FinalGrade = null },
            new Enrollment { StudentId = 3, CourseId = 1, EnrolledAt = new DateTime(2024, 3, 1), ProgressPercentage = 100, Status = EnrollmentStatus.Completed, FinalGrade = 88.00m },
            new Enrollment { StudentId = 3, CourseId = 3, EnrolledAt = new DateTime(2024, 3, 15), ProgressPercentage = 10, Status = EnrollmentStatus.Active, FinalGrade = null },
            new Enrollment { StudentId = 4, CourseId = 5, EnrolledAt = new DateTime(2024, 4, 25), ProgressPercentage = 5, Status = EnrollmentStatus.Dropped, FinalGrade = null });
    }
}