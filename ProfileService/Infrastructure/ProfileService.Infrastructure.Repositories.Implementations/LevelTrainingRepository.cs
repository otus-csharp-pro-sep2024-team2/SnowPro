using Microsoft.EntityFrameworkCore;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Domain.Entities;
using ProfileService.Infrastructure.EntityFramework;

namespace ProfileService.Infrastructure.Repositories.Implementations;

public class LevelTrainingRepository : Repository<LevelTraining, int>, ILevelTrainingRepository
{
    public LevelTrainingRepository(DatabaseContext context) : base(context)
    {
    }

    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Уровень подготовки. </returns>
    public override async Task<LevelTraining> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<LevelTraining>().SingleOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    /// <summary>
    /// Получить сущность по Name.
    /// </summary>
    /// <param name="name"> Название сущности. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Уровень подготовки. </returns>
    public async Task<LevelTraining> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await Context.Set<LevelTraining>().SingleOrDefaultAsync(l => l.Name == name, cancellationToken);
    }

    /// <summary>
    /// Получить список уровней подготовки.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Список уровней подготовки. </returns>
    public async Task<List<LevelTraining>> GetPagedAsync(int page, int itemsPerPage)
    {
        return await GetAll()
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();
    }
}
