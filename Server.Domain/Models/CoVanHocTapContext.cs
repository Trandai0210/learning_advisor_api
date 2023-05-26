using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Server.Domain.Models
{
    public partial class CoVanHocTapContext : DbContext
    {
        public CoVanHocTapContext()
        {
        }

        public CoVanHocTapContext(DbContextOptions<CoVanHocTapContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-515VO3H\\SQLEXPRESS;Database=CoVanHocTap;Integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("answer");

                entity.HasIndex(e => e.Content, "UQ__answer__9A58D9BD48DE2B1C")
                    .IsUnique();

                entity.Property(e => e.AnswerId).HasColumnName("answerId");

                entity.Property(e => e.Content)
                    .HasMaxLength(500)
                    .HasColumnName("content");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("faculty");

                entity.Property(e => e.FacultyId).HasColumnName("facultyId");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");

                entity.Property(e => e.MessageId).HasColumnName("messageId");

                entity.Property(e => e.Content)
                    .HasMaxLength(500)
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.FacultyId).HasColumnName("facultyId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.FacultyId)
                    .HasConstraintName("FK__message__faculty__2D27B809");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__message__userId__2C3393D0");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permission");

                entity.Property(e => e.PermissionId).HasColumnName("permissionId");

                entity.Property(e => e.PermissionName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("permissionName");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("question");

                entity.HasIndex(e => e.Keyword, "UQ__question__3697F5A2A79D8539")
                    .IsUnique();

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.Property(e => e.AnswerId).HasColumnName("answerId");

                entity.Property(e => e.Keyword)
                    .HasMaxLength(100)
                    .HasColumnName("keyword");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("FK__question__answer__33D4B598");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Gmail, "UQ__users__493D0C0A9AACAA79")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("dob");

                entity.Property(e => e.Gmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("gmail");

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.PermissionId).HasColumnName("permissionId");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK__users__permissio__276EDEB3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
