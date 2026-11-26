using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineLearningPlatform.Data;

#nullable disable

namespace Assignment_3_C.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineLearningPlatform.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "All programming topics",
                            Name = "Programming"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Backend",
                            ParentCategoryId = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "ASP.NET Core",
                            Name = "ASP.NET",
                            ParentCategoryId = 2
                        },
                        new
                        {
                            Id = 4,
                            Name = "Frontend",
                            ParentCategoryId = 1
                        },
                        new
                        {
                            Id = 5,
                            Description = "UI / UX",
                            Name = "Design"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Empty category (used to test safe delete)",
                            Name = "Miscellaneous"
                        });
                });

            modelBuilder.Entity("OnlineLearningPlatform.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime?>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("InstructorId");

                    b.ToTable("Courses", null, t =>
                        {
                            t.HasCheckConstraint("CK_Courses_Price", "[Price] >= 0");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 3,
                            Description = "Build REST APIs",
                            InstructorId = 1,
                            IsPublished = true,
                            Level = "Intermediate",
                            Price = 49.99m,
                            PublishedDate = new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "ASP.NET Core Web API"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "Learn C# from zero",
                            InstructorId = 1,
                            IsPublished = true,
                            Level = "Beginner",
                            Price = 29.99m,
                            PublishedDate = new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "C# Fundamentals"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 4,
                            InstructorId = 2,
                            IsPublished = true,
                            Level = "Beginner",
                            Price = 39.99m,
                            PublishedDate = new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "React for Beginners"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 3,
                            Description = "Deep dive into EF Core",
                            InstructorId = 1,
                            IsPublished = false,
                            Level = "Advanced",
                            Price = 79.99m,
                            Title = "Advanced EF Core"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 5,
                            InstructorId = 3,
                            IsPublished = true,
                            Level = "Beginner",
                            Price = 19.99m,
                            PublishedDate = new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "UI Design Basics"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 2,
                            InstructorId = 2,
                            IsPublished = false,
                            Level = "Advanced",
                            Price = 99.99m,
                            Title = "Microservices with .NET"
                        });
                });

            modelBuilder.Entity("OnlineLearningPlatform.Models.Enrollment", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EnrolledAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("FinalGrade")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<int>("ProgressPercentage")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("Enrollments", null, t =>
                        {
                            t.HasCheckConstraint("CK_Enrollments_Progress", "[ProgressPercentage] >= 0 AND [ProgressPercentage] <= 100");
                        });

                    b.HasData(
                        new
                        {
                            StudentId = 1,
                            CourseId = 1,
                            EnrolledAt = new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProgressPercentage = 60,
                            Status = "Active"
                        },
                        new
                        {
                            StudentId = 1,
                            CourseId = 2,
                            EnrolledAt = new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FinalGrade = 92.50m,
                            ProgressPercentage = 100,
                            Status = "Completed"
                        },
                        new
                        {
                            StudentId = 2,
                            CourseId = 1,
                            EnrolledAt = new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProgressPercentage = 30,
                            Status = "Active"
                        },
                        new
                        {
                            StudentId = 3,
                            CourseId = 1,
                            EnrolledAt = new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FinalGrade = 88.00m,
                            ProgressPercentage = 100,
                            Status = "Completed"
                        },
                        new
                        {
                            StudentId = 3,
                            CourseId = 3,
                            EnrolledAt = new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProgressPercentage = 10,
                            Status = "Active"
                        },
                        new
                        {
                            StudentId = 4,
                            CourseId = 5,
                            EnrolledAt = new DateTime(2024, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProgressPercentage = 5,
                            Status = "Dropped"
                        });
                });

            modelBuilder.Entity("OnlineLearningPlatform.Models.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("JoinedAt")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Instructors", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bio = "Senior .NET developer",
                            Email = "ahmed.hassan@learn.com",
                            FullName = "Ahmed Hassan",
                            JoinedAt = new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Email = "sara.mostafa@learn.com",
                            FullName = "Sara Mostafa",
                            JoinedAt = new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Bio = "UI/UX designer",
                            Email = "omar.khaled@learn.com",
                            FullName = "Omar Khaled",
                            JoinedAt = new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("OnlineLearningPlatform.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Students", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(2000, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mona.ali@mail.com",
                            FullName = "Mona Ali"
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(1999, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "youssef.adel@mail.com",
                            FullName = "Youssef Adel"
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateTime(2001, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "nour.ibrahim@mail.com",
                            FullName = "Nour Ibrahim"
                        },
                        new
                        {
                            Id = 4,
                            DateOfBirth = new DateTime(1998, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "karim.samir@mail.com",
                            FullName = "Karim Samir"
                        });
                });

            modelBuilder.Entity("OnlineLearningPlatform.Models.Category", b =>
                {
                    b.HasOne("OnlineLearningPlatform.Models.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("OnlineLearningPlatform.Models.Course", b =>
                {
                    b.HasOne("OnlineLearningPlatform.Models.Category", "Category")
                        .WithMany("Courses")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OnlineLearningPlatform.Models.Instructor", "Instructor")
                        .WithMany("Courses")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("OnlineLearningPlatform.Models.Enrollment", b =>
                {
                    b.HasOne("OnlineLearningPlatform.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineLearningPlatform.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("OnlineLearningPlatform.Models.Category", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("OnlineLearningPlatform.Models.Course", b =>
                {
                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("OnlineLearningPlatform.Models.Instructor", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("OnlineLearningPlatform.Models.Student", b =>
                {
                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
