using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LessonService.Infrastructure.EF;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<LessonGroup> LessonGroups { get; set; }
    //public DbSet<Instructor> Instructors { get; set; }
    //public DbSet<Student> Students { get; set; }
    public DbSet<Person> Persons { get; set; }

    /// <summary>
    /// Constructor to configure the database context 
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<Person>()
           .HasDiscriminator<string>("Discriminator")
           .HasValue<Person>("Person")
           .HasValue<Student>("Student")
           .HasValue<Instructor>("Instructor");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        optionsBuilder.EnableSensitiveDataLogging(false);
        base.OnConfiguring(optionsBuilder);
    }

}