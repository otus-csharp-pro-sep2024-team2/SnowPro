using Microsoft.EntityFrameworkCore;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Domain.Entities;
using ProfileService.Infrastructure.EntityFramework;

namespace ProfileService.Infrastructure.Repositories.Implementations;

public class AchievementRepository : Repository<Achievement, int>, IAchievementRepository
{
    public AchievementRepository(DatabaseContext context) : base(context)
    {
    }

    /// <summary>
    /// Получить достижение по Id.
    /// </summary>
    /// <param name="id"> Id достижения. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Достижение </returns>
    public override async Task<Achievement?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<Achievement>()
            .Where(a => a.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Получить список достижений по id профиля
    /// </summary>
    /// <param name="profileInfoId"> Id профиля. </param>
    /// <returns> Список достижений пользователя. </returns>
    public async Task<IEnumerable<Achievement>> GetByProfileInfoIdAsync(Guid profileInfoId)
    {
        return await Context.Set<Achievement>()
            .Where(a => a.ProfileInfoId == profileInfoId)
            .ToListAsync();
    }

    /// <summary>
    /// Получить список профилей.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Список профилей. </returns>
    public async Task<List<Achievement>> GetPagedAsync(int page, int itemsPerPage)
    {
        var query = GetAll();
        return await query
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();
    }
}