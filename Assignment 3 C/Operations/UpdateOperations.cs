using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Models.Enums;

namespace OnlineLearningPlatform.Operations;

public static class UpdateOperations
{
    public static void ChangePrice(AppDbContext db, int courseId, decimal newPrice)
    {
        Console.WriteLine($"\n--- Change price of course {courseId} to {newPrice} ---");

        if (newPrice < 0)
        {
            Console.WriteLine("Invalid price: it cannot be negative.");
            return;
        }

        var course = db.Courses.Find(courseId);
        if (course is null)
        {
            Console.WriteLine("Course not found.");
            return;
        }

        var old = course.Price;
        course.Price = newPrice;
        db.SaveChanges();
        Console.WriteLine($"'{course.Title}' price changed from {old:F2} to {course.Price:F2}");
    }

    public static void UpdateProgress(AppDbContext db, int studentId, int courseId, int progress)
    {
        Console.WriteLine($"\n--- Update progress (student {studentId}, course {courseId}) -> {progress}% ---");

        if (progress < 0 || progress > 100)
        {
            Console.WriteLine("Rejected: progress must be between 0 and 100.");
            return;
        }

        var enrollment = db.Enrollments.Find(studentId, courseId);
        if (enrollment is null)
        {
            Console.WriteLine("Enrollment not found.");
            return;
        }

        enrollment.ProgressPercentage = progress;
        if (progress == 100)
            enrollment.Status = EnrollmentStatus.Completed;

        db.SaveChanges();
        Console.WriteLine($"Progress = {enrollment.ProgressPercentage}%, Status = {enrollment.Status}");
    }

    public static void PublishCourse(AppDbContext db, int courseId)
    {
        Console.WriteLine($"\n--- Publish course {courseId} ---");

        var course = db.Courses.Find(courseId);
        if (course is null)
        {
            Console.WriteLine("Course not found.");
            return;
        }
        if (course.IsPublished)
        {
            Console.WriteLine($"'{course.Title}' is already published.");
            return;
        }

        course.IsPublished = true;
        course.PublishedDate = DateTime.UtcNow;
        db.SaveChanges();
        Console.WriteLine($"'{course.Title}' published on {course.PublishedDate:yyyy-MM-dd}");
    }
}