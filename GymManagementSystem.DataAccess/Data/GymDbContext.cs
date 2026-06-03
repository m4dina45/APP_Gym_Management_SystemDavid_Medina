using GymManagementSystem.Domain.Entities;
using GymManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.DataAccess.Data
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<GymClass> GymClasses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Membership
            modelBuilder.Entity<Membership>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.MembershipType).IsRequired();
                entity.Property(e => e.MonthlyPrice).HasPrecision(10, 2);
                entity.Property(e => e.DurationMonths).IsRequired();

                entity.HasMany(e => e.Members)
                    .WithOne(m => m.Membership)
                    .HasForeignKey(m => m.MembershipId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Member
            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
                entity.Property(e => e.BirthDate).IsRequired();

                entity.HasOne(e => e.Membership)
                    .WithMany(m => m.Members)
                    .HasForeignKey(e => e.MembershipId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Enrollments)
                    .WithOne(en => en.Member)
                    .HasForeignKey(en => en.MemberId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Trainer
            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Specialty).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);

                entity.HasMany(e => e.GymClasses)
                    .WithOne(gc => gc.Trainer)
                    .HasForeignKey(gc => gc.TrainerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure GymClass
            modelBuilder.Entity<GymClass>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Schedule).IsRequired().HasMaxLength(100);
                entity.Property(e => e.MaxCapacity).IsRequired();

                entity.HasOne(e => e.Trainer)
                    .WithMany(t => t.GymClasses)
                    .HasForeignKey(e => e.TrainerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Enrollments)
                    .WithOne(en => en.GymClass)
                    .HasForeignKey(en => en.GymClassId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Enrollment (junction table)
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.GymClassId });
                entity.Property(e => e.EnrollmentDate).IsRequired();

                entity.HasOne(e => e.Member)
                    .WithMany(m => m.Enrollments)
                    .HasForeignKey(e => e.MemberId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.GymClass)
                    .WithMany(gc => gc.Enrollments)
                    .HasForeignKey(e => e.GymClassId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
