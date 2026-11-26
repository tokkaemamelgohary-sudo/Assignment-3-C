using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Data;

namespace OnlineLearningPlatform.Operations;

public static class DeleteOperations
{
    public static void Unenroll(AppDbContext db, int studentId, int courseId)
    {
        Console.WriteLine($"\n--- Unenroll student {studentId} from course {courseId} ---");

        var enrollment = db.Enrollments.Find(studentId, courseId);
        if (enrollment is null)
        {
            Console.WriteLine("Enrollment not found.");
            return;
        }

        db.Enrollments.Remove(enrollment);
        db.SaveChanges();
        Console.WriteLine("Enrollment deleted.");
    }

    public static void DeleteCategoryUnsafe(AppDbContext db, int categoryId)
    {
        Console.WriteLine($"\n--- Delete category {categoryId} (unsafe) ---");

        // العمليات اللي قبلها حمّلت الـ sub-categories في الـ tracker،
        // وEF كان بيصفّر الـ FK بتاعهم في الذاكرة ويمسح الأب. نفضّيه الأول
        // عشان قاعدة البيانات هي اللي تقرر.
        db.ChangeTracker.Clear();

        var category = db.Categories.Find(categoryId);
        if (category is null)
        {
            Console.WriteLine("Category not found.");
            return;
        }

        try
        {
            db.Categories.Remove(category);
            db.SaveChanges();
            Console.WriteLine("Category deleted.");
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}");
            Console.WriteLine($"Inner: {ex.InnerException?.Message}");
            db.ChangeTracker.Clear();
        }
    }

    public static void DeleteCategorySafe(AppDbContext db, int categoryId)
    {
        Console.WriteLine($"\n--- Delete category {categoryId} (safe) ---");

        var category = db.Categories.Find(categoryId);
        if (category is null)
        {
            Console.WriteLine("Category not found.");
            return;
        }

        if (db.Categories.Any(c => c.ParentCategoryId == categoryId))
        {
            Console.WriteLine($"Cannot delete '{category.Name}': it still has sub-categories.");
            return;
        }
        if (db.Courses.Any(c => c.CategoryId == categoryId))
        {
            Console.WriteLine($"Cannot delete '{category.Name}': it still has courses.");
            return;
        }

        db.Categories.Remove(category);
        db.SaveChanges();
        Console.WriteLine($"Category '{category.Name}' deleted.");
    }

    public static void DeleteCourse(AppDbContext db, int courseId)
    {
        Console.WriteLine($"\n--- Delete course {courseId} ---");

        var course = db.Courses.Find(courseId);
        if (course is null)
        {
            Console.WriteLine("Course not found.");
            return;
        }

        var before = db.Enrollments.Count(e => e.CourseId == courseId);
        Console.WriteLine($"Enrollments before delete: {before}");

        db.Courses.Remove(course);
        db.SaveChanges();

        var after = db.Enrollments.Count(e => e.CourseId == courseId);
        Console.WriteLine($"Enrollments after delete: {after}");

    }
}