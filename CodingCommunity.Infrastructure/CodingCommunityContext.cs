using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodingCommunity.Infrastructure
{
    public partial class CodingCommunityContext : DbContext
    {
        public CodingCommunityContext()
        {
        }

        public CodingCommunityContext(DbContextOptions<CodingCommunityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<TokenTable> TokenTables { get; set; } = null!;
        public virtual DbSet<UserTable> UserTables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-650JLI8\\SQLEXPRESS;Initial Catalog=CodingCommunity;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Answer__Question__1920BF5C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Answer__UserID__1A14E395");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Question__UserID__164452B1");

                entity.HasMany(d => d.Tags)
                    .WithMany(p => p.Questions)
                    .UsingEntity<Dictionary<string, object>>(
                        "QuestionTag",
                        l => l.HasOne<Tag>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__QuestionT__TagID__1DE57479"),
                        r => r.HasOne<Question>().WithMany().HasForeignKey("QuestionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__QuestionT__Quest__1CF15040"),
                        j =>
                        {
                            j.HasKey("QuestionId", "TagId").HasName("PK__Question__DB97A028BCB8EF86");

                            j.ToTable("QuestionTag");

                            j.IndexerProperty<int>("QuestionId").HasColumnName("QuestionID");

                            j.IndexerProperty<int>("TagId").HasColumnName("TagID");
                        });
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TagName).HasMaxLength(255);
            });

            modelBuilder.Entity<TokenTable>(entity =>
            {
                entity.HasKey(e => e.TokenId)
                    .HasName("PK__TokenTab__658FEE8A6239EECC");

                entity.ToTable("TokenTable");

                entity.Property(e => e.TokenId).HasColumnName("TokenID");

                entity.Property(e => e.ExpirationUtc).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TokenTables)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TokenTabl__UserI__20C1E124");
            });

            modelBuilder.Entity<UserTable>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserTabl__1788CCACEA6AA552");

                entity.ToTable("UserTable");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Username).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
