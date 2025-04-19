using AutoMapper;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.AchievementContracts;
using ProfileService.Application.Contracts.FileAchievementContracts;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Services;

public class AchievementServiceApp : IAchievementServiceApp
{
    private readonly IMapper _mapper;
    private readonly IAchievementRepository _achievementRepository;
    private readonly IProfileInfoRepository _profileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AchievementServiceApp(
            IMapper mapper,
            IAchievementRepository achievementRepository,
            IProfileInfoRepository profileRepository,
            IUnitOfWork unitOfWork
        )
    {
        _mapper = mapper;
        _achievementRepository = achievementRepository;
        _profileRepository = profileRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Получить достижение со списком файлов.
    /// </summary>
    /// <param name="id"> Идентификатор достижения. </param>
    /// <returns> ДТО достижения со списком файлов. </returns>
    public async Task<AchievementDto> GetByIdAsync(int id)
    {

        var achievement = await _achievementRepository.GetAsync(id, CancellationToken.None);
        return _mapper.Map<Achievement, AchievementDto>(achievement);
    }

    /// <summary>
    /// Получить достижения пользователя со списком файлов.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns> Список ДТО достижений со списком файлов. </returns>
    public async Task<IEnumerable<AchievementDto>> GetByUserIdAsync(Guid userId)
    {
        var profileInfoId = _profileRepository.GetByUserIdAsync(userId, CancellationToken.None).Result.Id;

        var achievement = await _achievementRepository.GetByProfileInfoIdAsync(profileInfoId);
        return _mapper.Map<IEnumerable<Achievement>, IEnumerable<AchievementDto>>(achievement);
    }

    /// <summary>
    /// Создать достижение пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="creatingAchievement"> ДТО создаваемого достижения. </param>
    /// <returns> Идентификатор достижения. </returns>
    public async Task<int> CreateAsync(Guid userId, CreatingAchievementDto creatingAchievement)
    {
        Achievement achievement = _mapper.Map<CreatingAchievementDto, Achievement>(creatingAchievement);
        ProfileInfo? result = _profileRepository.GetByUserIdAsync(userId, CancellationToken.None).Result;
        if(result == null)
        {
            throw new Exception($"Профиль пользователя с идентфикатором {userId} не найден");
        }
        achievement.ProfileInfoId = result.Id;

        Achievement createdAchievement = await _achievementRepository.AddAsync(achievement);
        await _achievementRepository.SaveChangesAsync();
        return createdAchievement.Id;
    }

    /// <summary>
    /// Создать достижение со списком файлов.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="creatingAchievementWithFilesDto"> ДТО создаваемого достижения со списком файлов. </param>
    /// <returns> Идентификатор созданного достижения. </returns>
    public async Task<int> CreateWithFilesAsync(Guid userId, CreatingAchievementWithFilesDto creatingAchievementWithFilesDto)
    {
        Achievement createdAchievement = _mapper.Map<CreatingAchievementWithFilesDto, Achievement>(creatingAchievementWithFilesDto);
        ProfileInfo? result = _profileRepository.GetByUserIdAsync(userId, CancellationToken.None).Result;
        if (result == null)
        {
            throw new Exception($"Профиль пользователя с идентфикатором {userId} не найден");
        }
        createdAchievement.ProfileInfoId = result.Id;

        if (creatingAchievementWithFilesDto.FilesAchievement != null)
        {
            createdAchievement.FilesAchievement = _mapper.Map<IEnumerable<CreatingFileAchievementDto>, IEnumerable<FileAchievement>>(creatingAchievementWithFilesDto.FilesAchievement);
        }

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            createdAchievement = await _unitOfWork.AchievementRepository.AddAsync(createdAchievement);
            if (createdAchievement.FilesAchievement != null)
            {
                foreach (var file in createdAchievement.FilesAchievement)
                {
                    await _unitOfWork.FileAchievementRepository.AddAsync(file);
                }
            }

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return createdAchievement.Id;
    }

    /// <summary>
    /// Изменить достижение.
    /// </summary>
    /// <param name="id"> Идентификатор достижения. </param>
    /// <param name="updatingAchievementDto"> ДТО редактируемого достижения. </param>
    public async Task UpdateAsync(int id, UpdatingAchievementDto updatingAchievementDto)
    {
        Achievement achievement = await _achievementRepository.GetAsync(id, CancellationToken.None);
        if (achievement == null)
        {
            throw new Exception($"Достижение с идентфикатором {id} не найдено");
        }

        achievement.Title = updatingAchievementDto.Title;
        achievement.Description = updatingAchievementDto.Description;
        _achievementRepository.Update(achievement);
        await _achievementRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить достижение со списком файлов.
    /// </summary>
    /// <param name="id"> Идентификатор достижения. </param>
    public async Task DeleteAsync(int id)
    {
        var achievement = await _achievementRepository.GetAsync(id, CancellationToken.None);
        if (achievement == null)
        {
            throw new Exception($"Достижение с идентфикатором {id} не найдено");
        }

        _achievementRepository.Delete(achievement);
        await _achievementRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Получить постраничный список.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns></returns>
    public async Task<ICollection<AchievementDto>> GetPagedAsync(int page, int itemsPerPage)
    {
        if (page <= 0)
        {
            throw new ArgumentException("Номер страницы должен быть больше 0", nameof(page));
        }

        if (itemsPerPage <= 0)
        {
            throw new ArgumentException("Количество элементов на странице должно быть больше 0", nameof(itemsPerPage));
        }

        ICollection<Achievement> entities = await _achievementRepository.GetPagedAsync(page, itemsPerPage);
        return _mapper.Map<ICollection<Achievement>, ICollection<AchievementDto>>(entities);
    }
}