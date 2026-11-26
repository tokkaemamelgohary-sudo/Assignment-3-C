using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Operations;

namespace OnlineLearningPlatform;

public class Program
{
    public static void Main()
    {
        using var db = new AppDbContext();

        try
        {
            var instructorId = CreateOperations.AddInstructor(db);

            var newCourseId = CreateOperations.AddCourse(db, instructorId, categoryId: 3);
            CreateOperations.AddCourse(db, instructorId, categoryId: 999);  
            CreateOperations.AddCourse(db, 999, categoryId: 3);

            var studentIds = CreateOperations.AddStudents(db);

            CreateOperations.EnrollStudent(db, studentIds[0], 1);         
            CreateOperations.EnrollStudent(db, studentIds[0], 1);           
            CreateOperations.EnrollStudent(db, studentIds[0], 4);        
            CreateOperations.EnrollStudent(db, 999, 1);                 
            CreateOperations.EnrollStudent(db, studentIds[0], 999);         


            ReadOperations.FindCourse(db, 1, "ASP.NET Core Web API");
            ReadOperations.FindCourse(db, 999, "Not a real title");
            ReadOperations.FilterCourses(db, 20m, 60m);
            ReadOperations.ShowSubCategories(db, 1);
            ReadOperations.CourseRoster(db, 1);

            UpdateOperations.ChangePrice(db, newCourseId!.Value, 64.99m);
            UpdateOperations.UpdateProgress(db, studentIds[0], 1, 50);
            UpdateOperations.UpdateProgress(db, studentIds[0], 1, 150);    
            UpdateOperations.UpdateProgress(db, studentIds[0], 1, 100);     
            UpdateOperations.PublishCourse(db, newCourseId.Value);

            DeleteOperations.Unenroll(db, studentIds[0], 1);

            DeleteOperations.DeleteCategoryUnsafe(db, 1);  
            DeleteOperations.DeleteCategorySafe(db, 1);    
            DeleteOperations.DeleteCategorySafe(db, 6);    

            CreateOperations.EnrollStudent(db, studentIds[1], newCourseId.Value);
            CreateOperations.EnrollStudent(db, studentIds[2], newCourseId.Value);
            DeleteOperations.DeleteCourse(db, newCourseId.Value);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nUnexpected error: {ex.Message}");
            if (ex.InnerException is not null)
                Console.WriteLine($"Inner: {ex.InnerException.Message}");
        }
    }
}