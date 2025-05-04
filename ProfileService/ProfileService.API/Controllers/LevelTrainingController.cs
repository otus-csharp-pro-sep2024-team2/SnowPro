using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProfileService.API.Models.LevelTrainingModels;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.LevelTrainingContracts;

namespace ProfileService.API.Controllers;

[ApiController]
[EnableCors("AllowReactApp")]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class LevelTrainingController : ControllerBase
{
    private readonly ILevelTrainingServiceApp _service;
    private readonly IMapper _mapper;
    private readonly ILogger<LevelTrainingController> _logger;
    public LevelTrainingController(ILevelTrainingServiceApp service, ILogger<LevelTrainingController> logger, IMapper mapper)
    {
        _service = service;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить уровень подготовки.
    /// </summary>
    /// <param name="id"> Идентификатор уровня подготовки. </param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        return Ok(_mapper.Map<LevelTrainingModel>(await _service.GetByIdAsync(id)));
    }

    /// <summary>
    /// Создать уровень подготовки.
    /// </summary>
    /// <param name="creatingLevelTrainingModel"> Модель создаваемого уровня подготовки. </param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreatingLevelTrainingModel creatingLevelTrainingModel)
    {
        return Ok(await _service.CreateAsync(_mapper.Map<CreatingLevelTrainingDto>(creatingLevelTrainingModel)));
    }

    /// <summary>
    /// Изменить уровень подготовки.
    /// </summary>
    /// <param name="id"> Идентификатор уровня подготовки. </param>
    /// <param name="levelTrainingModel"> Модель редактируемого уровня подготовки. </param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdatingLevelTrainingModel levelTrainingModel)
    {
        try
        {
            await _service.UpdateAsync(id, _mapper.Map<UpdatingLevelTrainingModel, UpdatingLevelTrainingDto>(levelTrainingModel));
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Удалить уровень подготовки.
    /// </summary>
    /// <param name="id"> Идентификатор уровня подготовки. </param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить постраничный список.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<IActionResult> GetListAsync(int page, int itemsPerPage)
    {
        try
        {
            return Ok(_mapper.Map<List<LevelTrainingModel>>(await _service.GetPagedAsync(page, itemsPerPage)));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
