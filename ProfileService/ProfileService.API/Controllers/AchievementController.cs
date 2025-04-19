using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProfileService.API.Models.AchievementModels;
using ProfileService.API.Models.FileAchievementModels;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.AchievementContracts;
using ProfileService.Application.Contracts.FileAchievementContracts;

namespace ProfileService.API.Controllers;

[ApiController]
[EnableCors("AllowReactApp")]
[Route("api/[controller]")]
[Authorize]
public class AchievementController : ControllerBase
{
    private readonly IAchievementServiceApp _achievementService;
    private readonly IFileAchievementServiceApp _fileAchievementService;
    private readonly IMapper _mapper;
    private readonly ILogger<AchievementController> _logger;
    public AchievementController(
        IAchievementServiceApp achievementService,
        IFileAchievementServiceApp fileAchievementService,
        ILogger<AchievementController> logger,
        IMapper mapper
    )
    {
        _achievementService = achievementService;
        _fileAchievementService = fileAchievementService;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить достижение
    /// </summary>
    /// <param name="id"> Идентификатор достижения. </param>
    /// <returns> Достижение. </returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        return Ok(_mapper.Map<AchievementModel>(await _achievementService.GetByIdAsync(id)));
    }

    /// <summary>
    /// Получить список достижений пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns> Список достижений пользователя. </returns>
    [HttpGet("listUserAchievements")]
    public async Task<IActionResult> GetByUserIdAsync(Guid userId)
    {
        return Ok(_mapper.Map<IEnumerable<AchievementModel>>(await _achievementService.GetByUserIdAsync(userId)));
    }

    /// <summary>
    /// Создать достижение пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="achievementModel">Модель создаваемого достижения</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(Guid userId, CreatingAchievementModel achievementModel)
    {
        try
        {
            return Ok(await _achievementService.CreateAsync(userId, _mapper.Map<CreatingAchievementDto>(achievementModel)));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Создать достижение пользователя со списком файлов.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="achievementModel"></param>
    /// <returns></returns>
    [HttpPost("createWithFiles")]
    public async Task<IActionResult> CreateWithFilesAsync(Guid userId, CreatingAchievementWithFilesModel achievementModel)
    {
        try
        {
            return Ok(await _achievementService.CreateWithFilesAsync(userId, _mapper.Map<CreatingAchievementWithFilesDto>(achievementModel)));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Изменить достижение.
    /// </summary>
    /// <param name="id"> Идентификатор достижения. </param>
    /// <param name="achievementModel"> Модель редактируемого достижения. </param>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdatingAchievementModel achievementModel)
    {
        try
        {
            await _achievementService.UpdateAsync(id, _mapper.Map<UpdatingAchievementModel, UpdatingAchievementDto>(achievementModel));
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Удалить достижение со списком файлов.
    /// </summary>
    /// <param name="id"> Идентификатор достижения. </param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _achievementService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить постраничный список достижений.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns></returns>
    [HttpGet("listAchievements")]
    public async Task<IActionResult> GetListAsync(int page, int itemsPerPage)
    {
        try
        {
            return Ok(_mapper.Map<List<AchievementModel>>(await _achievementService.GetPagedAsync(page, itemsPerPage)));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить список id файлов достижения
    /// </summary>
    /// <param name="id">Идентификатор достижения</param>
    /// <returns></returns>
    [HttpGet("{id:int}/files")]
    public async Task<IActionResult> GetFileAsync(int id)
    {
        return Ok(_mapper.Map<IEnumerable<FileAchievementModel>>(await _fileAchievementService.GetByAchievementIdAsync(id)));
    }

    /// <summary>
    /// Добавить id файла достижения
    /// </summary>
    /// <param name="id">Идентификатор достижения</param>
    /// <param name="achievementModel">Модель создания файла достижения</param>
    /// <returns></returns>
    [HttpPost("{id:int}/addFile")]
    public async Task<IActionResult> CreateFileAsync(int id, CreatingFileAchievementModel achievementModel)
    {
        return Ok(await _fileAchievementService.CreateAsync(id, _mapper.Map<CreatingFileAchievementDto>(achievementModel)));
    }

    /// <summary>
    /// Удалить файл достижения
    /// </summary>
    /// <param name="fileId"> Идентификатор файла достижения. </param>
    /// <returns></returns>
    [HttpDelete("deleteFile/{fileId:int}")]
    public async Task<IActionResult> DeleteFileAsync(int fileId)
    {
        try
        {
            await _fileAchievementService.DeleteAsync(fileId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
