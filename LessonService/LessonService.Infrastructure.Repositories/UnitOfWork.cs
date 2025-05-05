using LessonService.Infrastructure.EF;
using LessonService.Interfaces;

namespace LessonService.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext context, ILessonRepository lessonRepository) : IUnitOfWork
{
    public ILessonRepository Lessons { get; } = lessonRepository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}