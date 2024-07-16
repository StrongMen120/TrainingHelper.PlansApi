using Microsoft.EntityFrameworkCore;
using Npgsql;
using Training.API.Plans.Persistance.TrainingsDb.Entities;

namespace Training.API.Plans.Persistance.TrainingsDb;

public sealed class TrainingDbContext : DbContext
{
    protected TrainingDbContext()
    { }

    public TrainingDbContext(DbContextOptions<TrainingDbContext> options) 
        : base(options)
    { }

   public DbSet<ExerciseInfoEntity> ExercisesInfo { get; init; } = null!; 
   public DbSet<ExerciseRecordsEntity> ExerciseRecords { get; init; } = null!; 
   public DbSet<ExerciseRecordsRegistryEntity> ExerciseRecordsRegistry { get; init; } = null!; 
   public DbSet<DoneExercisesEntity> DoneExercises { get; init; } = null!;
   public DbSet<PlannedExercisesEntity> PlannedExercises { get; init; } = null!;
   public DbSet<PlannedTrainingsEntity> PlannedTrainings { get; init; } = null!;
   public DbSet<PlansEntity> Plans { get; init; } = null!;
   public DbSet<FileEntity> Files { get; init; } = null!;
   public DbSet<TestEmployerEntity> TestEmployers { get; init; } = null!;
    private static void RegisterEnums()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<PlansType>($"{TrainingDatabaseConstants.DefaultSchema}.{nameof(PlansType)}");
        NpgsqlConnection.GlobalTypeMapper.MapEnum<PlansImage>($"{TrainingDatabaseConstants.DefaultSchema}.{nameof(PlansImage)}");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        RegisterEnums();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<PlansType>(TrainingDatabaseConstants.DefaultSchema, nameof(PlansType));
        modelBuilder.HasPostgresEnum<PlansImage>(TrainingDatabaseConstants.DefaultSchema, nameof(PlansImage));
        modelBuilder.HasDefaultSchema(TrainingDatabaseConstants.DefaultSchema);
        modelBuilder.Entity<PlansEntity>(entity =>
        {
            entity.HasKey(a => a.Identifier);
            entity.HasMany(a => a.PlannedExercises).WithOne(e => e.Plans).HasForeignKey(e => e.PlansId);
        });
        modelBuilder.Entity<PlannedTrainingsEntity>(entity =>
        {
            entity.HasKey(a => a.Identifier);
            entity.HasOne(a => a.Plans).WithMany(a => a.PlannedTrainings).HasForeignKey(e => e.PlansId);
        });
        modelBuilder.Entity<PlannedExercisesEntity>(entity =>
        {
            entity.HasKey(a => a.Identifier);
            entity.HasOne(a => a.ExerciseInfo).WithMany(a => a.PlannedExercises).HasForeignKey(a => a.ExerciseInfoId);
        });
        modelBuilder.Entity<ExerciseInfoEntity>(entity =>
        {
            entity.HasKey(a => a.Identifier);
        });
        modelBuilder.Entity<DoneExercisesEntity>(entity =>
        {
            entity.HasKey(a => a.Identifier);
            entity.HasOne(a => a.ExerciseInfo).WithMany(a => a.DoneExercises).HasForeignKey(a => a.ExerciseInfoId);
        });
        modelBuilder.Entity<ExerciseRecordsEntity>(entity =>
        {
            entity.HasKey(a => new { a.Identifier, a.Revision });
            entity.HasOne(a => a.RegistryEntry).WithMany(a => a.ExerciseRecords).HasForeignKey(a => a.Identifier);
        });
        modelBuilder.Entity<ExerciseRecordsRegistryEntity>(entity =>
        {
            entity.HasOne(a => a.ExerciseInfo).WithMany(a => a.CurrentRecords).HasForeignKey(a => a.ExerciseId);
            entity.HasMany(a => a.ExerciseRecords).WithOne(e => e.RegistryEntry).HasForeignKey(e => e.Identifier);
            entity.HasKey(a => a.Identifier);
            entity.Property(a => a.Identifier).ValueGeneratedOnAdd();
        });
    }
}
