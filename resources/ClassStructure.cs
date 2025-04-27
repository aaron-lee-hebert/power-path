using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Workout531.Models
{
    public class User
    {
        public User()
        {
            UserId = Guid.NewGuid();
        }

        [Key]
        public Guid UserId { get; set; }
        
        [Required, MaxLength(50)]
        public string Username { get; set; }
        
        [Required, MaxLength(100)]
        public string Email { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }
        
        public DateTime DateCreated { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ICollection<TrainingCycle> TrainingCycles { get; set; }
        public virtual ICollection<UserMax> UserMaxes { get; set; }
        public virtual ICollection<ProgressRecord> ProgressRecords { get; set; }
        public virtual ICollection<WorkoutDay> WorkoutDays { get; set; }
    }

    public class ExerciseType
    {
        public ExerciseType()
        {
            ExerciseTypeId = Guid.NewGuid();
        }

        [Key]
        public Guid ExerciseTypeId { get; set; }
        
        [Required, MaxLength(50)]
        public string Name { get; set; }
        
        [Required, MaxLength(50)]
        public string Category { get; set; } // Main or Assistance
        
        public string Description { get; set; }

        // Navigation properties
        public virtual ICollection<UserMax> UserMaxes { get; set; }
        public virtual ICollection<ProgrammedSet> ProgrammedSets { get; set; }
        public virtual ICollection<ProgressRecord> ProgressRecords { get; set; }
    }

    public class TrainingCycle
    {
        public TrainingCycle()
        {
            CycleId = Guid.NewGuid();
        }

        [Key]
        public Guid CycleId { get; set; }
        
        public Guid UserId { get; set; }
        
        public int CycleNumber { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public decimal RoundingFactor { get; set; } = 2.5m;
        
        public string Notes { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual ICollection<WorkoutWeek> WorkoutWeeks { get; set; }
        public virtual ICollection<UserMax> CycleMaxes { get; set; }
    }

    public class UserMax
    {
        public UserMax()
        {
            UserMaxId = Guid.NewGuid();
        }

        [Key]
        public Guid UserMaxId { get; set; }
        
        public Guid UserId { get; set; }
        
        public Guid ExerciseTypeId { get; set; }
        
        public Guid CycleId { get; set; }
        
        public decimal ActualOneRepMax { get; set; }
        
        [NotMapped]
        public decimal TrainingMax => Math.Floor(ActualOneRepMax * 0.9m / RoundingFactor) * RoundingFactor;
        
        public decimal? EstimatedOneRepMax { get; set; }
        
        public decimal RoundingFactor { get; set; } = 2.5m;
        
        public DateTime DateRecorded { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual User User { get; set; }
        public virtual ExerciseType ExerciseType { get; set; }
        public virtual TrainingCycle Cycle { get; set; }
    }

    public class WorkoutWeek
    {
        public WorkoutWeek()
        {
            WeekId = Guid.NewGuid();
        }

        [Key]
        public Guid WeekId { get; set; }
        
        public Guid CycleId { get; set; }
        
        public int WeekNumber { get; set; } // 1, 2, 3, or 4 for deload
        
        public string WeekType { get; set; } // Standard, Deload

        // Navigation properties
        public virtual TrainingCycle Cycle { get; set; }
        public virtual ICollection<WorkoutDay> WorkoutDays { get; set; }
    }

    public class WorkoutDay
    {
        public WorkoutDay()
        {
            WorkoutDayId = Guid.NewGuid();
        }

        [Key]
        public Guid WorkoutDayId { get; set; }
        
        public Guid UserId { get; set; }
        
        public Guid WeekId { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Status { get; set; } // Planned, Completed, Skipped
        
        public string Notes { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual WorkoutWeek Week { get; set; }
        public virtual ICollection<ProgrammedSet> ProgrammedSets { get; set; }
    }

    public class ProgrammedSet
    {
        public ProgrammedSet()
        {
            SetId = Guid.NewGuid();
        }

        [Key]
        public Guid SetId { get; set; }
        
        public Guid WorkoutDayId { get; set; }
        
        public Guid ExerciseTypeId { get; set; }
        
        public decimal PercentageOfTM { get; set; }
        
        public int TargetReps { get; set; }
        
        public int SetOrder { get; set; }
        
        public bool IsAmrap { get; set; } // For 5+, 3+, 1+ sets

        // Navigation properties
        public virtual WorkoutDay WorkoutDay { get; set; }
        public virtual ExerciseType ExerciseType { get; set; }
        public virtual CompletedSet CompletedSet { get; set; }
    }

    public class CompletedSet
    {
        public CompletedSet()
        {
            CompletedSetId = Guid.NewGuid();
        }

        [Key]
        public Guid CompletedSetId { get; set; }
        
        public Guid ProgrammedSetId { get; set; }
        
        public decimal ActualWeight { get; set; }
        
        public int ActualReps { get; set; }
        
        public int? RPE { get; set; } // Rate of Perceived Exertion (optional)
        
        public string Notes { get; set; }

        // Navigation properties
        public virtual ProgrammedSet ProgrammedSet { get; set; }
    }

    public class ProgressRecord
    {
        public ProgressRecord()
        {
            RecordId = Guid.NewGuid();
        }

        [Key]
        public Guid RecordId { get; set; }
        
        public Guid UserId { get; set; }
        
        public Guid ExerciseTypeId { get; set; }
        
        public DateTime Date { get; set; } = DateTime.Now;
        
        public decimal Weight { get; set; }
        
        public int Reps { get; set; }
        
        [NotMapped]
        public decimal EstimatedOneRepMax => Weight * (1 + (decimal)Reps / 30); // Simplified Epley formula
        
        public bool IsPersonalRecord { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual ExerciseType ExerciseType { get; set; }
    }

    // DbContext for Entity Framework Core
    public class Workout531Context : DbContext
    {
        public Workout531Context(DbContextOptions<Workout531Context> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ExerciseType> ExerciseTypes { get; set; }
        public DbSet<TrainingCycle> TrainingCycles { get; set; }
        public DbSet<UserMax> UserMaxes { get; set; }
        public DbSet<WorkoutWeek> WorkoutWeeks { get; set; }
        public DbSet<WorkoutDay> WorkoutDays { get; set; }
        public DbSet<ProgrammedSet> ProgrammedSets { get; set; }
        public DbSet<CompletedSet> CompletedSets { get; set; }
        public DbSet<ProgressRecord> ProgressRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships
            modelBuilder.Entity<User>()
                .HasMany(u => u.TrainingCycles)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrainingCycle>()
                .HasMany(c => c.WorkoutWeeks)
                .WithOne(w => w.Cycle)
                .HasForeignKey(w => w.CycleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutWeek>()
                .HasMany(w => w.WorkoutDays)
                .WithOne(d => d.Week)
                .HasForeignKey(d => d.WeekId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutDay>()
                .HasMany(d => d.ProgrammedSets)
                .WithOne(s => s.WorkoutDay)
                .HasForeignKey(s => s.WorkoutDayId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProgrammedSet>()
                .HasOne(s => s.CompletedSet)
                .WithOne(c => c.ProgrammedSet)
                .HasForeignKey<CompletedSet>(c => c.ProgrammedSetId)
                .OnDelete(DeleteBehavior.Cascade);

            // Set up unique constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<ExerciseType>()
                .HasIndex(e => e.Name)
                .IsUnique();

            modelBuilder.Entity<TrainingCycle>()
                .HasIndex(c => new { c.UserId, c.CycleNumber })
                .IsUnique();

            modelBuilder.Entity<WorkoutWeek>()
                .HasIndex(w => new { w.CycleId, w.WeekNumber })
                .IsUnique();

            modelBuilder.Entity<UserMax>()
                .HasIndex(um => new { um.UserId, um.ExerciseTypeId, um.CycleId })
                .IsUnique();

            modelBuilder.Entity<ProgrammedSet>()
                .HasIndex(ps => new { ps.WorkoutDayId, ps.ExerciseTypeId, ps.SetOrder })
                .IsUnique();

            // Seed some exercise types with explicit GUIDs for consistency
            var squatId = Guid.Parse("621a26fc-5d8f-4661-a170-52c8eda8c71b");
            var benchId = Guid.Parse("3f184a45-9f3a-4cea-a187-d26a24edd7f2");
            var deadliftId = Guid.Parse("5f69c790-4dab-4c5c-a141-8a3f64c35f5a");
            var pressId = Guid.Parse("c0c69fc2-9e5a-4982-8c64-9c1814f4fe12");
            var dipsId = Guid.Parse("d7bd8222-0eda-44c1-b946-2734b96dc7a6");
            var pullUpsId = Guid.Parse("3fb71c73-a676-4c3d-a4c8-5346350b4792");

            modelBuilder.Entity<ExerciseType>().HasData(
                new ExerciseType { ExerciseTypeId = squatId, Name = "Squat", Category = "Main", Description = "Barbell back squat" },
                new ExerciseType { ExerciseTypeId = benchId, Name = "Bench", Category = "Main", Description = "Barbell bench press" },
                new ExerciseType { ExerciseTypeId = deadliftId, Name = "Deadlift", Category = "Main", Description = "Barbell deadlift" },
                new ExerciseType { ExerciseTypeId = pressId, Name = "Press", Category = "Main", Description = "Overhead press" },
                new ExerciseType { ExerciseTypeId = dipsId, Name = "Dips", Category = "Assistance", Description = "Bodyweight or weighted dips" },
                new ExerciseType { ExerciseTypeId = pullUpsId, Name = "Pull-ups", Category = "Assistance", Description = "Bodyweight or weighted pull-ups" }
            );
        }
    }
}