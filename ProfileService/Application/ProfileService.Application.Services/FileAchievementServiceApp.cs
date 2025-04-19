using AutoMapper;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.FileAchievementContracts;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Services;

public class FileAchievementServiceApp : IFileAchievementServiceApp
{
    private readonly IMapper _mapper;
    private readonly IFileAchievementRepository _fileAchievementRepository;

    public FileAchievementServiceApp(
            IMapper mapper,
            IFileAchievementRepository fileAchievementRepository
        )
    {
        _mapper = mapper;
        _fileAchievementRepository = fileAchievementRepository;
    }

    /// <summary>
    /// Получить файл достижения.
    /// </summary>
    /// <param name="id"> Идентификатор файла достижения. </param>
    /// <returns> ДТО файла достижения. </returns>
    public async Task<IEnumerable<FileAchievementDto>> GetByAchievementIdAsync(int id)
    {
        List<FileAchievement> achievement = await _fileAchievementRepository.GetByAchievementIdAsync(id, CancellationToken.None);
        return _mapper.Map<ICollection<FileAchievement>, ICollection<FileAchievementDto>>(achievement);
    }


    /// <summary>
    /// Создать файл достижения.
    /// </summary>
    /// <param name="achievementId"> id достижения. </param>
    /// <param name="creatingFileAchievementDto"> ДТО создаваемого файла достижения. </param>
    /// <returns>id файла достижения</returns>
    public async Task<int> CreateAsync(int achievementId, CreatingFileAchievementDto creatingFileAchievementDto)
    {
        FileAchievement achievement = _mapper.Map<CreatingFileAchievementDto, FileAchievement>(creatingFileAchievementDto);
        achievement.AchievementId = achievementId;

        var createdFileAchievement = await _fileAchievementRepository.AddAsync(achievement);
        await _fileAchievementRepository.SaveChangesAsync();
        return createdFileAchievement.Id;
    }

    /// <summary>
    /// Удалить файл достижения.
    /// </summary>
    /// <param name="id"> Идентификатор файла достижения. </param>
    /// <returns></returns>
    public async Task DeleteAsync(int id)
    {
        var fileAchievement = await _fileAchievementRepository.GetAsync(id, CancellationToken.None);
        if (fileAchievement == null)
        {
            throw new Exception($"Файл достижения с идентфикатором {id} не найден");
        }

        _fileAchievementRepository.Delete(fileAchievement);
        await _fileAchievementRepository.SaveChangesAsync();
    }
}
