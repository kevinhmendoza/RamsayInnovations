using Microsoft.EntityFrameworkCore;
using RamsayInnovations.Domain;

#nullable disable

namespace RamsayInnovations.Infrastructure
{
    public partial class Sample_DBContext : DbContext
    {
      
        public Sample_DBContext()
        {
          
        }

        public Sample_DBContext(DbContextOptions<Sample_DBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=sample_DB.db3");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(
                entity =>
            {
                entity.ToTable("Student");

                entity.HasIndex(e => e.Username, "IX_Student_Username")
                    .IsUnique();

                entity.Property(e => e.Career)
                    .IsRequired()
                    .HasColumnType("VARCHAR(50)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("VARCHAR(20)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("VARCHAR(20)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("VARCHAR(20)");
            }
            );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
