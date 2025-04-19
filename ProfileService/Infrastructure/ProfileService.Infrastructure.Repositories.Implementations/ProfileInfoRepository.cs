using Microsoft.EntityFrameworkCore;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Domain.Entities;
using ProfileService.Infrastructure.EntityFramework;

namespace ProfileService.Infrastructure.Repositories.Implementations;

public class ProfileInfoRepository : Repository<ProfileInfo, Guid>, IProfileInfoRepository
{
    public ProfileInfoRepository(DatabaseContext context) : base(context)
    {
    }

    /// <summary>
    /// Получить профиль.
    /// </summary>
    /// <param name="id"> Идентификатор профиля. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Профиль. </returns>
    public override async Task<ProfileInfo> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = Context.Set<ProfileInfo>().AsQueryable();
        query = query
            .Where(p => p.Id == id && !p.IsDeleted && p.IsCurrentVersion);

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Получить профиль пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Профиль пользователя. </returns>
    public async Task<ProfileInfo?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Context.Set<ProfileInfo>()
            .SingleOrDefaultAsync(p => p.UserId == userId && !p.IsDeleted && p.IsCurrentVersion);
    }

    /// <summary>
    /// Получить список профилей.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Список профилей. </returns>
    public async Task<List<ProfileInfo>> GetPagedAsync(int page, int itemsPerPage)
    {
        var query = GetAll().Where(p => !p.IsDeleted && p.IsCurrentVersion);
        return await query
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();
    }
}
