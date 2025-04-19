using AutoMapper;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.ProfileInfoContracts;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Common.Enums;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Services;

/// <summary>
/// Cервис работы с профилями.
/// </summary>
public class ProfileInfoServiceApp : IProfileInfoServiceApp
{
    private readonly IMapper _mapper;
    private readonly IProfileInfoRepository _profileRepository;
    private readonly ITypeSportEquipmentRepository _typeSportEquipmentRepository;
    private readonly ILevelTrainingRepository _levelTrainingRepository;

    public ProfileInfoServiceApp(
            IMapper mapper,
            IProfileInfoRepository profileRepository,
            ITypeSportEquipmentRepository typeSportEquipmentRepository,
            ILevelTrainingRepository levelTrainingRepository
        )
    {
        _mapper = mapper;
        _profileRepository = profileRepository;
        _typeSportEquipmentRepository = typeSportEquipmentRepository;
        _levelTrainingRepository = levelTrainingRepository;
    }

    /// <summary>
    /// Получить профиль.
    /// </summary>
    /// <param name="id"> Идентификатор профиля. </param>
    /// <returns> ДТО профиля. </returns>
    public async Task<ProfileInfoDto> GetByIdAsync(Guid id)
    {
        var profile = await _profileRepository.GetAsync(id, CancellationToken.None);
        return _mapper.Map<ProfileInfo, ProfileInfoDto>(profile);
    }

    /// <summary>
    /// Получить профиль пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns> ДТО профиля. </returns>
    public async Task<ProfileInfoDto> GetByUserIdAsync(Guid userId)
    {
        var profile = await _profileRepository.GetByUserIdAsync(userId, CancellationToken.None);
        return _mapper.Map<ProfileInfo, ProfileInfoDto>(profile);
    }

    /// <summary>
    /// Создать профиль.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="creatingProfileDto"> ДТО создаваемого профиля. </param>
    public async Task<Guid> CreateAsync(Guid userId, CreatingProfileInfoDto creatingProfileDto)
    {
        ProfileInfo profile = _mapper.Map<CreatingProfileInfoDto, ProfileInfo>(creatingProfileDto);

        profile.Id = Guid.NewGuid();
        profile.VersionNumber = 1;
        profile.IsCurrentVersion = true;
        profile.UserId = userId;
        profile.CreatedDate = DateTime.UtcNow;
        profile.Status = ProfileStatuses.Created;
        profile.IsActive = true;
        profile.IsDeleted = false;
       
        if (creatingProfileDto.TypeSportEquipmentProfile != null)
        {
            profile.TypeSportEquipmentProfile = new List<TypeSportEquipmentProfile>();
            foreach (var p in creatingProfileDto.TypeSportEquipmentProfile)
            {
                TypeSportEquipment? typeSportEquipment = _typeSportEquipmentRepository.GetByNameAsync(p.TypeSportEquipmentName, CancellationToken.None).Result;
                LevelTraining? levelTraining = _levelTrainingRepository.GetByNameAsync(p.LevelTrainingName, CancellationToken.None).Result;

                if (typeSportEquipment == null || levelTraining == null)
                {
                    throw new ArgumentException("Не найдены TypeSportEquipmentName или LevelTrainingName", nameof(creatingProfileDto.TypeSportEquipmentProfile));
                }

                profile.TypeSportEquipmentProfile.Append(
                    new TypeSportEquipmentProfile
                    { 
                        ProfileId = profile.Id,
                        TypeSportEquipmentId = typeSportEquipment.Id,
                        TypeSportEquipment = typeSportEquipment,
                        LevelTrainingId = levelTraining.Id,
                        LevelTraining = levelTraining
                    });
            }
        }
        ProfileInfo createdClientProfile = await _profileRepository.AddAsync(profile);
        await _profileRepository.SaveChangesAsync();
        return createdClientProfile.Id;
    }

    /// <summary>
    /// Изменить профиль.
    /// </summary>
    /// <param name="id"> Идентификатор профиля. </param>
    /// <param name="updatingProfileDto"> ДТО редактируемого профиля. </param>
    public async Task UpdateAsync(Guid id, UpdatingProfileInfoDto updatingProfileDto)
    {
        ProfileInfo? profile = await _profileRepository.GetAsync(id, CancellationToken.None);
        if (profile == null)
        {
            throw new Exception($"Профиль с идентфикатором {id} не найден");
        }

        profile.UpdatedDate = DateTime.UtcNow;
        profile.Status = ProfileStatuses.Changed;
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
    /// Удалить профиль.
    /// </summary>
    /// <param name="id"> Идентификатор профиля. </param>
    /// <returns></returns>
    public async Task<ProfileInfoDto> DeleteAsync(Guid id)
    {
        var profile = await _profileRepository.GetAsync(id, CancellationToken.None);
        if (profile == null)
        {
            throw new Exception($"Профиль с идентфикатором {id} не найден");
        }

        profile.UpdatedDate = DateTime.UtcNow;
        profile.Status = ProfileStatuses.Hidden;
        profile.IsActive = false;
        profile.IsDeleted = true;
        profile.UpdatedUserId = profile.UserId;
        await _profileRepository.SaveChangesAsync();
        return _mapper.Map<ProfileInfo, ProfileInfoDto>(profile);
    }

    /// <summary>
    /// Получить постраничный список профилей.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns></returns>
    public async Task<ICollection<ProfileInfoDto>> GetPagedAsync(int page, int itemsPerPage)
    {
        if (page <= 0)
        {
            throw new ArgumentException("Номер страницы должен быть больше 0", nameof(page));
        }

        if (itemsPerPage <= 0)
        {
            throw new ArgumentException("Количество элементов на странице должно быть больше 0", nameof(itemsPerPage));
        }

        ICollection<ProfileInfo> entities = await _profileRepository.GetPagedAsync(page, itemsPerPage);
        return _mapper.Map<ICollection<ProfileInfo>, ICollection<ProfileInfoDto>>(entities);
    }
}
