using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfileService.Common.Enums;
using ProfileService.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProfileService.Infrastructure.EntityFramework;

/// <summary>
/// Контекст.
/// </summary>
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<ProfileInfo> Profiles { get; set; } = null!;
    public DbSet<ClientProfileInfo> ClientProfiles { get; set; } = null!;
    public DbSet<InstructorProfileInfo> InstructorProfiles { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<TypeSportEquipment> TypesSportEquipment { get; set; } = null!;
    public DbSet<LevelTraining> LevelsTraining { get; set; } = null!;
    public DbSet<Achievement> Achievements { get; set; } = null!;
    public DbSet<FileAchievement> FilesAchievement { get; set; } = null!;
    public DbSet<TypeSportEquipmentProfile> TypesSportEquipmentProfiles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProfileInfo>()
            .ToTable(nameof(Profiles))
            .HasDiscriminator(p => p.ProfileType)
            .HasValue<ClientProfileInfo>(ProfileType.Client)
            .HasValue<InstructorProfileInfo>(ProfileType.Instructor)
            .HasValue<ProfileInfo>(ProfileType.Profile);

        modelBuilder.Entity<ProfileInfo>()
            .HasIndex(p => new { p.ProfileType, p.IsCurrentVersion });

        modelBuilder.Entity<Achievement>()
            .HasOne(a => a.ProfileInfo)
            .WithMany(p => p.Achievements)
            .HasForeignKey(a => a.ProfileInfoId);

        modelBuilder.Entity<FileAchievement>()
            .HasOne(a => a.Achievement)
            .WithMany(a => a.FilesAchievement)
            .HasForeignKey(a => a.AchievementId);

        modelBuilder.Entity<ProfileInfo>()
            .HasMany(t => t.TypeSportEquipment)
            .WithMany(p => p.ProfileInfo)
            .UsingEntity<TypeSportEquipmentProfile>(
                t => t.HasOne(t => t.TypeSportEquipment)
                .WithMany(t => t.TypeSportEquipmentProfile)
                .HasForeignKey(t => t.TypeSportEquipmentId),
                p => p.HasOne(p => p.ProfileInfo)
                .WithMany(p => p.TypeSportEquipmentProfile)
                .HasForeignKey(p => p.ProfileId),
                t => t.HasOne(t => t.LevelTraining)
                    .WithMany(l => l.TypeSportEquipmentProfile)
                    .HasForeignKey(t => t.LevelTrainingId)
            );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }
}