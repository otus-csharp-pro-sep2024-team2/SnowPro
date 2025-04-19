using Microsoft.EntityFrameworkCore;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Common.Enums;
using ProfileService.Domain.Entities;
using ProfileService.Infrastructure.EntityFramework;

namespace ProfileService.Infrastructure.Repositories.Implementations;

public class InstructorProfileInfoRepository : Repository<InstructorProfileInfo, Guid>, IInstructorProfileInfoRepository
{
    public InstructorProfileInfoRepository(DatabaseContext context) : base(context)
    {
    }

    /// <summary>
    /// Получить профиль инструктора по Id.
    /// </summary>
    /// <param name="id"> Id профиля инструктора. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Профиль инструктора. </returns>
    public override async Task<InstructorProfileInfo?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context
            .Set<InstructorProfileInfo>()
            .OfType<InstructorProfileInfo>()
            .Where(i => !i.IsDeleted && i.IsActive && i.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Получить профиль инструктора по Id пользователя.
    /// </summary>
    /// <param name="userId"> Id пользователя. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Профиль инструктора. </returns>
    public async Task<InstructorProfileInfo> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await Context
            .Set<InstructorProfileInfo>()
            .OfType<InstructorProfileInfo>()
            .Where(i => i.UserId == userId && !i.IsDeleted && i.IsActive && i.IsCurrentVersion)
            .OrderByDescending(i => i.CreatedDate)
            .SingleOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Получить профиль инструктора по Id пользователя и статусу профиля.
    /// </summary>
    /// <param name="userId"> Id пользователя. </param>
    /// <param name="profileStatus"> Статус профиля. </param>
    /// <returns> Профиль инструктора. </returns>
    public async Task<InstructorProfileInfo?> GetByUserIdAndStatusAsync(Guid userId, ProfileStatuses profileStatus)
    {
        return await Context
           .Set<InstructorProfileInfo>()
           .OfType<InstructorProfileInfo>()
           .Where(i => i.UserId == userId && !i.IsDeleted && i.Status == profileStatus)
           .OrderByDescending(i => i.CreatedDate)
           .SingleOrDefaultAsync();
    }

    /// <summary>
    /// Получить список профилей.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Список профилей. </returns>
    public async Task<List<InstructorProfileInfo>> GetPagedAsync(int page, int itemsPerPage)
    {
        var query = GetAll().OfType<InstructorProfileInfo>().Where(i => !i.IsDeleted && i.IsActive && i.IsCurrentVersion);
        return await query
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();
    }

    /// <summary>
    /// Получить cписок профилей инструктора требующих подтверждение изменений
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns></returns>
    public async Task<List<InstructorProfileInfo>> GetRequiredConfirmationPagedAsync(int page, int itemsPerPage)
    {
        var query = GetAll().OfType<InstructorProfileInfo>()
            .Where(i => !i.IsDeleted && i.Status == ProfileStatuses.RequiredConfirmation);
        return await query
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();
    }
}