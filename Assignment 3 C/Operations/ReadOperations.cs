using OnlineLearningPlatform.Data;

namespace OnlineLearningPlatform.Operations;

public static class ReadOperations
{
    public static void FindCourse(AppDbContext db, int id, string title)
    {
        Console.WriteLine($"\n--- Find course (Id = {id}, Title = '{title}') ---");

        var byId = db.Courses.Find(id);
        Console.WriteLine(byId is null
            ? $"Find({id}): not found"
            : $"Find({id}): {byId.Title} - {byId.Price:F2}");

        var byTitle = db.Courses.FirstOrDefault(c => c.Title == title);
        Console.WriteLine(byTitle is null
            ? $"FirstOrDefault('{title}'): not found"
            : $"FirstOrDefault('{title}'): Id = {byTitle.Id}, {byTitle.Level}");
    }

    public static void FilterCourses(AppDbContext db, decimal min, decimal max)
    {
        Console.WriteLine($"\n--- Courses priced between {min} and {max} (price desc) ---");

        var courses = db.Courses
            .Where(c => c.Price >= min && c.Price <= max)
            .OrderByDescending(c => c.Price)
            .Select(c => new { c.Title, c.Level, c.Price })  
            .ToList();

        if (courses.Count == 0)
        {
            Console.WriteLine("No courses in this price range.");
            return;
        }

        foreach (var c in courses)
            Console.WriteLine($"{c.Title,-30} {c.Level,-14} {c.Price,8:F2}");
    }

    public static void ShowSubCategories(AppDbContext db, int parentId)
    {
        Console.WriteLine($"\n--- Sub-categories of category {parentId} ---");

        var parent = db.Categories.Find(parentId);
        if (parent is null)
        {
            Console.WriteLine("Category not found.");
        }
        else
        {
            var subs = db.Categories.Where(c => c.ParentCategoryId == parentId).ToList();
            Console.WriteLine($"Direct sub-categories of '{parent.Name}': {subs.Count}");
            foreach (var s in subs) Console.WriteLine($"  - {s.Name}");
        }

        Console.WriteLine("Top-level categories:");
        var top = db.Categories.Where(c => c.ParentCategoryId == null).ToList();
        foreach (var t in top) Console.WriteLine($"  - {t.Name}");
    }

    public static void CourseRoster(AppDbContext db, int courseId)
    {
        Console.WriteLine($"\n--- Roster of course {courseId} ---");

        if (!db.Courses.Any(c => c.Id == courseId))
        {
            Console.WriteLine("Course not found.");
            return;
        }

        var studentIds = db.Enrollments
            .Where(e => e.CourseId == courseId)
            .Select(e => e.StudentId)
            .ToList();

        var students = db.Students
            .Where(s => studentIds.Contains(s.Id))
            .ToList();

        Console.WriteLine($"Enrolled students: {students.Count}");
        foreach (var s in students) Console.WriteLine($"  - {s.FullName}");
    }
}