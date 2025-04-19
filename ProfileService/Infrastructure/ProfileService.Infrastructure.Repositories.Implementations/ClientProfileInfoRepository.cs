using Microsoft.EntityFrameworkCore;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Domain.Entities;
using ProfileService.Infrastructure.EntityFramework;


namespace ProfileService.Infrastructure.Repositories.Implementations;

public class ClientProfileInfoRepository : Repository<ClientProfileInfo, Guid>, IClientProfileInfoRepository
{
    public ClientProfileInfoRepository(DatabaseContext context) : base(context)
    {
    }

    /// <summary>
    /// Получить профиль клиента по Id.
    /// </summary>
    /// <param name="id"> Id профиля клиента. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Профиль клиента. </returns>
    public override async Task<ClientProfileInfo?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context
            .Set<ClientProfileInfo>()
            .OfType<ClientProfileInfo>()
            .Where(c => c.Id == id && !c.IsDeleted && c.IsCurrentVersion)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Получить профиль клиента по Id пользователя.
    /// </summary>
    /// <param name="userId"> Id пользователя. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Профиль клиента. </returns>
    public async Task<ClientProfileInfo?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Context
            .Set<ClientProfileInfo>()
            .OfType<ClientProfileInfo>()
            .Where(c => !c.IsDeleted &&
                c.UserId == userId &&
                c.IsCurrentVersion
            )
            //.OrderByDescending(c => c.CreatedDate)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Получить список профилей.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Список профилей. </returns>
    public async Task<IReadOnlyList<ClientProfileInfo>> GetPagedAsync(int page, int itemsPerPage)
    {
        var query = GetAll()
            .OfType<ClientProfileInfo>()
            .Where(c => !c.IsDeleted && c.IsCurrentVersion);
        return await query
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();
    }
}