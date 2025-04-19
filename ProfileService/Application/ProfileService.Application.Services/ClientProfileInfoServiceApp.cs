using AutoMapper;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.ClientProfileInfoContracts;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Common.Enums;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Services;

/// <summary>
/// Cервис работы с профилями клиентов.
/// </summary>
public class ClientProfileInfoServiceApp : IClientProfileInfoServiceApp
{
    private readonly IMapper _mapper;
    private readonly IClientProfileInfoRepository _profileRepository;
    //private readonly IBusControl _busControl;
    //private readonly IUnitOfWork _unitOfWork;

    public ClientProfileInfoServiceApp(
            IMapper mapper,
            IClientProfileInfoRepository profileRepository
        //IUnitOfWork unitOfWork,
        //IBusControl busControl
        )
    {
        _mapper = mapper;
        _profileRepository = profileRepository;
        //_busControl = busControl;
        //_unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Получить профиль клиента.
    /// </summary>
    /// <param name="id"> Идентификатор профиля клиента. </param>
    /// <returns> ДТО профиля клиента. </returns>
    public async Task<ClientProfileInfoDto> GetByIdAsync(Guid id)
    {
        var clientProfile = await _profileRepository.GetAsync(id, CancellationToken.None);
        return _mapper.Map<ClientProfileInfo, ClientProfileInfoDto>(clientProfile);
    }

    /// <summary>
    /// Получить профиль клиента.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns> ДТО профиля клиента. </returns>
    public async Task<ClientProfileInfoDto> GetByUserIdAsync(Guid userId)
    {
        var clientProfile = await _profileRepository.GetByUserIdAsync(userId, CancellationToken.None);
        return _mapper.Map<ClientProfileInfo?, ClientProfileInfoDto>(clientProfile);
    }

    /// <summary>
    /// Создать профиль клиента.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="creatingProfileDto"> ДТО создаваемого профиля клиента. </param>
    public async Task<Guid> CreateAsync(Guid userId, CreatingClientProfileInfoDto creatingProfileDto)
    {
        return await CreateWithOwnerAsync(userId, null, creatingProfileDto);
    }

    /// <summary>
    /// Создать профиль клиента.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="ownerId"> Идентификатор профиля. </param>
    /// <param name="creatingProfileDto"> ДТО создаваемого профиля клиента. </param>
    public async Task<Guid> CreateWithOwnerAsync(Guid userId, Guid? ownerId, CreatingClientProfileInfoDto creatingProfileDto)
    {
        var clientProfile = _mapper.Map<CreatingClientProfileInfoDto, ClientProfileInfo>(creatingProfileDto);
        clientProfile.Id = Guid.NewGuid();
        clientProfile.VersionNumber = 1;
        clientProfile.IsCurrentVersion = true;
        clientProfile.UserId = userId;
        clientProfile.CreatedDate = DateTime.UtcNow;
        clientProfile.Status = ProfileStatuses.Created;
        clientProfile.IsActive = true;
        clientProfile.IsDeleted = false;
        clientProfile.OwnerProfileInfoId = ownerId;
        var createdClientProfile = await _profileRepository.AddAsync(clientProfile);
        await _profileRepository.SaveChangesAsync();
        return createdClientProfile.Id;
    }

    /// <summary>
    /// Изменить профиль клиента.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="updatingProfileDto"> ДТО редактируемого профиля клиента. </param>
    public async Task UpdateAsync(Guid userId, UpdatingClientProfileInfoDto updatingProfileDto)
    {
        var profile = await _profileRepository.GetByUserIdAsync(userId, CancellationToken.None);
        if (profile == null)
        {
            throw new Exception($"Профиль клиента с идентфикатором {userId} не найден");
        }

        profile.UpdatedDate = DateTime.UtcNow;
        profile.Status = updatingProfileDto.Status;
        profile.IsActive = updatingProfileDto.IsActive;
        profile.IsDeleted = updatingProfileDto.IsDeleted;
        profile.UpdatedUserId = updatingProfileDto.UpdatedUserId;
        profile.PhotoId = updatingProfileDto.PhotoId;
        profile.Surname = updatingProfileDto.Surname;
        profile.Name = updatingProfileDto.Name;
        profile.Patronymic = updatingProfileDto.Patronymic;
        profile.BirthDate = updatingProfileDto.BirthDate;
        profile.Gender = updatingProfileDto.Gender;
        profile.PhoneNumber = updatingProfileDto.PhoneNumber;
        profile.TelegramName = updatingProfileDto.TelegramName;

        _profileRepository.Update(profile);
        await _profileRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить профиль клиента.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    public async Task<ClientProfileInfoDto> DeleteAsync(Guid userId)
    {
        ClientProfileInfo? profile = await _profileRepository.GetByUserIdAsync(userId, CancellationToken.None);

        if (profile == null)
        {
            throw new Exception($"Профиль клиента с идентфикатором {userId} не найден");
        }

        profile.UpdatedDate = DateTime.UtcNow;
        profile.Status = ProfileStatuses.Hidden;
        profile.IsActive = false;
        profile.IsDeleted = true;
        profile.UpdatedUserId = userId;

        await _profileRepository.SaveChangesAsync();

        return _mapper.Map<ClientProfileInfo, ClientProfileInfoDto>(profile);
    }

    /// <summary>
    /// Получить постраничный список профилей пользователя.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Страница профилей пользователя. </returns>
    public async Task<IReadOnlyList<ClientProfileInfoDto>> GetPagedAsync(int page, int itemsPerPage)
    {
        if (page <= 0)
        {
            throw new ArgumentException("Номер страницы должен быть больше 0", nameof(page));
        }

        if (itemsPerPage <= 0)
        {
            throw new ArgumentException("Количество элементов на странице должно быть больше 0", nameof(itemsPerPage));
        }

        IReadOnlyList<ClientProfileInfo?> entities = await _profileRepository.GetPagedAsync(page, itemsPerPage);
        return _mapper.Map<IReadOnlyList<ClientProfileInfo?>, IReadOnlyList<ClientProfileInfoDto>>(entities);
    }
}