using Microsoft.EntityFrameworkCore;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Domain.Entities;
using ProfileService.Infrastructure.EntityFramework;

namespace ProfileService.Infrastructure.Repositories.Implementations;

public class PositionRepository: Repository<Position, int>, IPositionRepository
{
    public PositionRepository(DatabaseContext context) : base(context)
    {
    }

    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Должность. </returns>
    public override async Task<Position> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<Position>().SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    /// <summary>
    /// Получить список должностей.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Список должностей. </returns>
    public async Task<List<Position>> GetPagedAsync(int page, int itemsPerPage)
    {
        return await GetAll()
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();
    }
}

