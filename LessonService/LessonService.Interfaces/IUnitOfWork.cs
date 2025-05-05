namespace LessonService.Interfaces;

public interface IUnitOfWork
{
    ILessonRepository Lessons { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}