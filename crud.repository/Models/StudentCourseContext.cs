using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace crud.repository.Models;

public partial class StudentCourseContext : DbContext
{
    public StudentCourseContext()
    {
    }

    public StudentCourseContext(DbContextOptions<StudentCourseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PCA117\\SQLEXPRESS;Database=StudentCourse;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Courseid).HasName("PK__course__C9D27D8F827BE9D3");

            entity.ToTable("course");

            entity.Property(e => e.CourseContent)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CourseName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CreatedById).HasDefaultValue(0);
            entity.Property(e => e.DeletedById).HasDefaultValue(0);
            entity.Property(e => e.Department)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EditedById).HasDefaultValue(0);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
