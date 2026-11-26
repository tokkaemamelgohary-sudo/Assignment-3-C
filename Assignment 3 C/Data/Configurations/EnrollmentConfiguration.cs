using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Data.Configurations;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments", t =>
            t.HasCheckConstraint("CK_Enrollments_Progress",
                "[ProgressPercentage] >= 0 AND [ProgressPercentage] <= 100"));

        builder.HasKey(e => new { e.StudentId, e.CourseId });

        builder.Property(e => e.EnrolledAt).IsRequired();
        builder.Property(e => e.ProgressPercentage).IsRequired();
        builder.Property(e => e.Status)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20);
        builder.Property(e => e.FinalGrade).HasPrecision(5, 2);

    }
}