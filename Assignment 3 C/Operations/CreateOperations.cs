using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Models;
using OnlineLearningPlatform.Models.Enums;

namespace OnlineLearningPlatform.Operations;

public static class CreateOperations
{
    public static int AddInstructor(AppDbContext db)
    {
        Console.WriteLine("\n--- Add instructor ---");
        var instructor = new Instructor
        {
            FullName = "Mahmoud Fathy",
            Email = $"mahmoud.{Guid.NewGuid().ToString("N")[..6]}@learn.com",
            Bio = "Cloud & DevOps instructor",
            JoinedAt = DateTime.Today
        };

        db.Instructors.Add(instructor);
        db.SaveChanges();
        Console.WriteLine($"Instructor added. Generated Id = {instructor.Id}");
        return instructor.Id;
    }

    public static int? AddCourse(AppDbContext db, int instructorId, int categoryId)
    {
        Console.WriteLine($"\n--- Add course (instructor {instructorId}, category {categoryId}) ---");

        if (!db.Instructors.Any(i => i.Id == instructorId))
        {
            Console.WriteLine($"Cannot add course: instructor {instructorId} does not exist.");
            return null;
        }
        if (!db.Categories.Any(c => c.Id == categoryId))
        {
            Console.WriteLine($"Cannot add course: category {categoryId} does not exist.");
            return null;
        }

        var course = new Course
        {
            Title = "Clean Architecture in .NET",
            Description = "Layers, DDD and CQRS",
            Price = 59.99m,
            Level = CourseLevel.Advanced,
            IsPublished = false,
            PublishedDate = null,
            InstructorId = instructorId,
            CategoryId = categoryId
        };

        db.Courses.Add(course);
        db.SaveChanges();
        Console.WriteLine($"Course added. Generated Id = {course.Id}");
        return course.Id;
    }

    public static List<int> AddStudents(AppDbContext db)
    {
        Console.WriteLine("\n--- Add several students ---");
        var suffix = Guid.NewGuid().ToString("N")[..6];

        var students = new List<Student>
        {
            new() { FullName = "Hana Tarek",   Email = $"hana.{suffix}@mail.com",  DateOfBirth = new DateTime(2002, 1, 20) },
            new() { FullName = "Ziad Mansour", Email = $"ziad.{suffix}@mail.com",  DateOfBirth = new DateTime(2000, 9, 3) },
            new() { FullName = "Laila Samy",   Email = $"laila.{suffix}@mail.com", DateOfBirth = new DateTime(1999, 12, 25) }
        };

        db.Students.AddRange(students);   
        db.SaveChanges();                

        foreach (var s in students)
            Console.WriteLine($"Student '{s.FullName}' -> Id = {s.Id}");

        return students.Select(s => s.Id).ToList();
    }

    public static void EnrollStudent(AppDbContext db, int studentId, int courseId)
    {
        Console.WriteLine($"\n--- Enroll student {studentId} in course {courseId} ---");

        if (!db.Students.Any(s => s.Id == studentId))
        {
            Console.WriteLine("Refused: student does not exist.");
            return;
        }

        var course = db.Courses.Find(courseId);
        if (course is null)
        {
            Console.WriteLine("Refused: course does not exist.");
            return;
        }

        if (!course.IsPublished)
        {
            Console.WriteLine($"Refused: course '{course.Title}' is not published.");
            return;
        }

        if (db.Enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId))
        {
            Console.WriteLine("Refused: student is already enrolled in this course.");
            return;
        }

        db.Enrollments.Add(new Enrollment
        {
            StudentId = studentId,
            CourseId = courseId,
            EnrolledAt = DateTime.UtcNow,
            ProgressPercentage = 0,
            Status = EnrollmentStatus.Active
        });
        db.SaveChanges();
        Console.WriteLine($"Student {studentId} enrolled in '{course.Title}'.");
    }
}