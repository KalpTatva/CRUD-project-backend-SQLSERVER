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

    public virtual DbSet<CourseStudentsInfo> CourseStudentsInfos { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAudit> UserAudits { get; set; }

    public virtual DbSet<UserCourseMapping> UserCourseMappings { get; set; }

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

        modelBuilder.Entity<CourseStudentsInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("course_students_info");

            entity.Property(e => e.CourseName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PK__RefreshT__658FEEEA2C164EF1");

            entity.ToTable("RefreshToken");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsRevoked).HasDefaultValue(false);
            entity.Property(e => e.IsUsed).HasDefaultValue(false);
            entity.Property(e => e.JwtId).IsUnicode(false);
            entity.Property(e => e.Token).IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RefreshTo__UserI__619B8048");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__1788CC4C24CD94B7");

            entity.ToTable("users", tb =>
                {
                    tb.HasTrigger("USER_AUDIT");
                    tb.HasTrigger("User_update_audit");
                });

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CreatedById).HasDefaultValue(0);
            entity.Property(e => e.DeletedById).HasDefaultValue(0);
            entity.Property(e => e.EditedById).HasDefaultValue(0);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasDefaultValue(0);
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserAudit>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__UserAudi__A17F23985A9661D3");

            entity.ToTable("UserAudit");

            entity.Property(e => e.Msg).IsUnicode(false);
            entity.Property(e => e.Operation)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<UserCourseMapping>(entity =>
        {
            entity.HasKey(e => e.MappingId).HasName("PK__UserCour__8B57819D280972B6");

            entity.ToTable("UserCourseMapping");

            entity.Property(e => e.CourseStatus).HasDefaultValue(1);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.CreatedById).HasDefaultValue(0);
            entity.Property(e => e.DeletedById).HasDefaultValue(0);
            entity.Property(e => e.EditedById).HasDefaultValue(0);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Course).WithMany(p => p.UserCourseMappings)
                .HasForeignKey(d => d.Courseid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserCours__Cours__33D4B598");

            entity.HasOne(d => d.User).WithMany(p => p.UserCourseMappings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserCours__UserI__34C8D9D1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
