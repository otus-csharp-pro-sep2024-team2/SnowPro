using AutoMapper;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.InstructorProfileInfoContracts;
using ProfileService.Application.Contracts.TypeSportEquipmentProfileInfoContracts;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Common.Enums;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Services;
/// <summary>
/// Cервис работы с профилями инструктора.
/// </summary>
public class InstructorProfileInfoServiceApp : IInstructorProfileInfoServiceApp
{
    private readonly IMapper _mapper;
    private readonly IInstructorProfileInfoRepository _instructorProfileRepository;
    private readonly ITypeSportEquipmentRepository _typeSportEquipmentRepository;
    private readonly ILevelTrainingRepository _levelTrainingRepository;
    private readonly IPositionRepository _positionRepository;

    public InstructorProfileInfoServiceApp(
            IMapper mapper,
            IInstructorProfileInfoRepository profileRepository,
            ITypeSportEquipmentRepository typeSportEquipmentRepository,
            ILevelTrainingRepository levelTrainingRepository,
            IPositionRepository positionRepository
        )
    {
        _mapper = mapper;
        _instructorProfileRepository = profileRepository;
        _typeSportEquipmentRepository = typeSportEquipmentRepository;
        _levelTrainingRepository = levelTrainingRepository;
        _positionRepository = positionRepository;
    }

    /// <summary>
    /// Получить профиль инструктора.
    /// </summary>
    /// <param name="id"> Идентификатор профиля инструктора. </param>
    /// <returns> ДТО профиля инструктора. </returns>
    public async Task<InstructorProfileInfoDto> GetByIdAsync(Guid id)
    {
        var instructorProfile = await _instructorProfileRepository.GetAsync(id, CancellationToken.None);
        return _mapper.Map<InstructorProfileInfo, InstructorProfileInfoDto>(instructorProfile);
    }

    /// <summary>
    /// Получить профиль инструктора по id пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns> ДТО профиля инструктора. </returns>
    public async Task<InstructorProfileInfoDto> GetByUserIdAsync(Guid userId)
    {
        var instructorProfile = await _instructorProfileRepository.GetByUserIdAsync(userId, CancellationToken.None);
        return _mapper.Map<InstructorProfileInfo?, InstructorProfileInfoDto>(instructorProfile);
    }

    /// <summary>
    /// Получить профиль инструктора по Id пользователя и статусу профиля.
    /// </summary>
    /// <param name="userId"> Id пользователя. </param>
    /// <param name="profileStatus"> Статус профиля. </param>
    /// <returns> Профиль инструктора. </returns>
    public async Task<InstructorProfileInfoDto> GetByUserIdAndStatusAsync(Guid userId, ProfileStatuses profileStatus)
    {
        var instructorProfile = await _instructorProfileRepository.GetByUserIdAndStatusAsync(userId, profileStatus);
        return _mapper.Map<InstructorProfileInfo?, InstructorProfileInfoDto>(instructorProfile);
    }

    /// <summary>
    /// Создать профиль инструктора.
    /// </summary>
    /// <param name="userId"> Id пользователя. </param>
    /// <param name="creatingInstructorProfileDto"> Профиль инструктора. </param>
    public async Task<Guid> CreateAsync(Guid userId, CreatingInstructorProfileInfoDto creatingInstructorProfileDto)
    {
        InstructorProfileInfo instructorProfile = _mapper.Map<CreatingInstructorProfileInfoDto, InstructorProfileInfo>(creatingInstructorProfileDto);

        try
        {
            if (creatingInstructorProfileDto.PositionId != null)
            {
                Position position = await _positionRepository.GetAsync((int)creatingInstructorProfileDto.PositionId, CancellationToken.None);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Должность {creatingInstructorProfileDto.PositionId} не найдена");
        }

        instructorProfile.Id = Guid.NewGuid();
        instructorProfile.VersionNumber = 1;
        instructorProfile.IsCurrentVersion = true;
        instructorProfile.UserId = userId;
        instructorProfile.CreatedDate = DateTime.UtcNow;
        instructorProfile.Status = ProfileStatuses.Created;
        instructorProfile.IsActive = true;
        instructorProfile.IsDeleted = false;

        var sportEquipmentProfile = new List<TypeSportEquipmentProfile>();

        if (creatingInstructorProfileDto.TypeSportEquipmentProfile != null)
        {
            foreach (CreatingTypeSportEquipmentProfileInfoDto sportEquipmentDto in creatingInstructorProfileDto.TypeSportEquipmentProfile)
            {
                TypeSportEquipmentProfile sportEquipment = _mapper.Map<CreatingTypeSportEquipmentProfileInfoDto, TypeSportEquipmentProfile>(sportEquipmentDto);
                sportEquipment.ProfileInfo = instructorProfile;

                if (sportEquipmentDto.TypeSportEquipmentName != null)
                {
                    sportEquipment.TypeSportEquipment = await _typeSportEquipmentRepository.GetByNameAsync(sportEquipmentDto.TypeSportEquipmentName, CancellationToken.None);
                }

                if (sportEquipmentDto.LevelTrainingName != null)
                {
                    sportEquipment.LevelTraining = await _levelTrainingRepository.GetByNameAsync(sportEquipmentDto.LevelTrainingName, CancellationToken.None);
                }
                sportEquipmentProfile.Add(sportEquipment);
            }
        }
        instructorProfile.TypeSportEquipmentProfile = sportEquipmentProfile;

        var createdInstructorProfile = await _instructorProfileRepository.AddAsync(instructorProfile);

        await _instructorProfileRepository.SaveChangesAsync();

        return createdInstructorProfile.Id;
    }

    /// <summary>
    /// Изменить профиль инструктора по id пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="updatingInstructorProfileDto"> ДТО редактируемого профиля инструктора. </param>
    public async Task UpdateAsync(Guid userId, UpdatingInstructorProfileInfoDto updatingInstructorProfileDto)
    {
        InstructorProfileInfo? currentInstructorProfile = await _instructorProfileRepository.GetByUserIdAsync(userId, CancellationToken.None);
        if (currentInstructorProfile == null)
        {
            throw new Exception($"Профиль инструктора пользователя с идентфикатором {userId} не найден");
        }

        InstructorProfileInfoDto? instructorProfileRequiredConfirmation = GetByUserIdAndStatusAsync(userId, ProfileStatuses.RequiredConfirmation).Result;

        if (currentInstructorProfile.Status == ProfileStatuses.Changed && instructorProfileRequiredConfirmation != null)
        {
            throw new Exception($"Изменения профиля инструктора не подтверждены. Изменение невозможно.");
        }

        currentInstructorProfile.UpdatedDate = DateTime.UtcNow;
        currentInstructorProfile.Status = ProfileStatuses.Changed;
        currentInstructorProfile.IsActive = true;
        currentInstructorProfile.IsDeleted = false;
        currentInstructorProfile.IsCurrentVersion = true;

        InstructorProfileInfo instructorProfile = _mapper.Map<UpdatingInstructorProfileInfoDto, InstructorProfileInfo>(updatingInstructorProfileDto);
        instructorProfile.Id = Guid.NewGuid();
        instructorProfile.VersionNumber = currentInstructorProfile.VersionNumber + 1;
        instructorProfile.IsCurrentVersion = false;
        instructorProfile.UserId = userId;
        instructorProfile.CreatedDate = DateTime.UtcNow;
        instructorProfile.Status = ProfileStatuses.RequiredConfirmation;
        instructorProfile.IsActive = false;
        instructorProfile.IsDeleted = false;

        var sportEquipmentProfile = new List<TypeSportEquipmentProfile>();

        if (updatingInstructorProfileDto.TypeSportEquipmentProfile != null)
        {
            foreach (CreatingTypeSportEquipmentProfileInfoDto sportEquipmentDto in updatingInstructorProfileDto.TypeSportEquipmentProfile)
            {
                TypeSportEquipmentProfile sportEquipment = _mapper.Map<CreatingTypeSportEquipmentProfileInfoDto, TypeSportEquipmentProfile>(sportEquipmentDto);
                sportEquipment.ProfileInfo = instructorProfile;

                if (sportEquipmentDto.TypeSportEquipmentName != null)
                {
                    sportEquipment.TypeSportEquipment = await _typeSportEquipmentRepository.GetByNameAsync(sportEquipmentDto.TypeSportEquipmentName, CancellationToken.None);
                }

                if (sportEquipmentDto.LevelTrainingName != null)
                {
                    sportEquipment.LevelTraining = await _levelTrainingRepository.GetByNameAsync(sportEquipmentDto.LevelTrainingName, CancellationToken.None);
                }
                sportEquipmentProfile.Add(sportEquipment);
            }
        }
        instructorProfile.TypeSportEquipmentProfile = sportEquipmentProfile;
        var createdInstructorProfile = await _instructorProfileRepository.AddAsync(instructorProfile);

        await _instructorProfileRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Подтверждение изменений профиля
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="profileStatus"> Статус профиля. </param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<InstructorProfileInfoDto> ConfirmСhangesAsync(Guid userId, ProfileStatuses profileStatus)
    {
        InstructorProfileInfo? currentInstructorProfile = await _instructorProfileRepository.GetByUserIdAsync(userId, CancellationToken.None);
        if (currentInstructorProfile == null)
        {
            throw new Exception($"Профиль инструктора пользователя с идентфикатором {userId} не найден");
        }

        if (currentInstructorProfile.Status != ProfileStatuses.RequiredConfirmation && currentInstructorProfile.Status != ProfileStatuses.Changed)
        {
            throw new Exception($"Изменения профиля инструктора не требуют подтверждения");
        }

        if (profileStatus != ProfileStatuses.Confirmed && profileStatus != ProfileStatuses.Rejected)
        {
            throw new Exception($"Некорректный статус профиля для подтверждения или отмены изменений");
        }

        InstructorProfileInfo? requiredConfirmationInstructorProfile = await _instructorProfileRepository.GetByUserIdAndStatusAsync(userId, ProfileStatuses.RequiredConfirmation);
        if (requiredConfirmationInstructorProfile == null)
        {
            throw new Exception($"Профиль инструктора, требующий подтверждения, пользователя с идентфикатором {userId} не найден");
        }

        bool isConfirmed = profileStatus == ProfileStatuses.Confirmed;

        currentInstructorProfile.IsCurrentVersion = !isConfirmed;
        currentInstructorProfile.IsActive = !isConfirmed;

        requiredConfirmationInstructorProfile.IsCurrentVersion = isConfirmed;
        requiredConfirmationInstructorProfile.IsActive = isConfirmed;
        requiredConfirmationInstructorProfile.Status = profileStatus;

        await _instructorProfileRepository.SaveChangesAsync();

        return _mapper.Map<InstructorProfileInfo, InstructorProfileInfoDto>(requiredConfirmationInstructorProfile);
    }

    /// <summary>
    /// Удалить профиль инструктора по id пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    public async Task<InstructorProfileInfoDto> DeleteAsync(Guid userId)
    {
        var instructorProfile = await _instructorProfileRepository.GetByUserIdAsync(userId, CancellationToken.None);
        if (instructorProfile == null)
        {
            throw new Exception($"Профиль инструктора пользователя с идентфикатором {userId} не найден");
        }
        instructorProfile.UpdatedDate = DateTime.UtcNow;
        instructorProfile.Status = ProfileStatuses.Hidden;
        instructorProfile.IsActive = false;
        instructorProfile.IsDeleted = true;
        instructorProfile.UpdatedUserId = userId;
        await _instructorProfileRepository.SaveChangesAsync();
        return _mapper.Map<InstructorProfileInfo, InstructorProfileInfoDto>(instructorProfile);
    }

    /// <summary>
    /// Получить постраничный список профилей инструктора.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Страница профилей инструктора. </returns>
    public async Task<ICollection<InstructorProfileInfoDto>> GetPagedAsync(int page, int itemsPerPage)
    {
        if (page <= 0)
        {
            throw new ArgumentException("Номер страницы должен быть больше 0", nameof(page));
        }

        if (itemsPerPage <= 0)
        {
            throw new ArgumentException("Количество элементов на странице должно быть больше 0", nameof(itemsPerPage));
        }

        ICollection<InstructorProfileInfo> entities = await _instructorProfileRepository.GetPagedAsync(page, itemsPerPage);
        return _mapper.Map<ICollection<InstructorProfileInfo>, ICollection<InstructorProfileInfoDto>>(entities);
    }

    /// <summary>
    /// Получить cписок профилей инструктора требующих подтверждение изменений
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns></returns>
    public async Task<ICollection<InstructorProfileInfoDto>> GetRequiredConfirmationAsync(int page, int itemsPerPage)
    {
        if (page <= 0)
        {
            throw new ArgumentException("Номер страницы должен быть больше 0", nameof(page));
        }

        if (itemsPerPage <= 0)
        {
            throw new ArgumentException("Количество элементов на странице должно быть больше 0", nameof(itemsPerPage));
        }

        ICollection<InstructorProfileInfo> entities = await _instructorProfileRepository.GetRequiredConfirmationPagedAsync(page, itemsPerPage);
        return _mapper.Map<ICollection<InstructorProfileInfo>, ICollection<InstructorProfileInfoDto>>(entities);
    }
}
