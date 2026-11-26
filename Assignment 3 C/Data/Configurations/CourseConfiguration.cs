using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Data.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses", t =>
            t.HasCheckConstraint("CK_Courses_Price", "[Price] >= 0"));
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Description).HasMaxLength(2000);
        builder.Property(c => c.Price).IsRequired().HasPrecision(10, 2);
        builder.Property(c => c.Level)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20);
        builder.Property(c => c.IsPublished).IsRequired();
        builder.Property(c => c.PublishedDate);

        builder.HasMany(c => c.Enrollments)
               .WithOne(e => e.Course)
               .HasForeignKey(e => e.CourseId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
    }
}