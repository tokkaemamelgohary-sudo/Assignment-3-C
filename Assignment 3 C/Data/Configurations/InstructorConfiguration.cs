using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Data.Configurations;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructors");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.FullName).IsRequired().HasMaxLength(100);
        builder.Property(i => i.Email).IsRequired().HasMaxLength(150);
        builder.Property(i => i.Bio).HasMaxLength(500);
        builder.Property(i => i.JoinedAt).IsRequired().HasColumnType("date");

        builder.HasIndex(i => i.Email).IsUnique();

        builder.HasMany(i => i.Courses)
               .WithOne(c => c.Instructor)
               .HasForeignKey(c => c.InstructorId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);
    }
}