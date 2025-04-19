using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProfileService.API.Models.PositionModels;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.PositionContracts;

namespace ProfileService.API.Controllers;

[ApiController]
[EnableCors("AllowReactApp")]
[Route("api/[controller]")]
[Authorize]
public class PositionController : ControllerBase
{
    private readonly IPositionServiceApp _service;
    private readonly IMapper _mapper;
    public PositionController(IPositionServiceApp service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить должность.
    /// </summary>
    /// <param name="id"> Идентификатор должности. </param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        return Ok(_mapper.Map<PositionModel>(await _service.GetByIdAsync(id)));
    }

    /// <summary>
    /// Создать должность.
    /// </summary>
    /// <param name="positionModel"> Модель создаваемой должности. </param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreatingPositionModel positionModel)
    {
        return Ok(await _service.CreateAsync(_mapper.Map<CreatingPositionDto>(positionModel)));
    }

    /// <summary>
    /// Изменить должность.
    /// </summary>
    /// <param name="id"> Идентификатор должности. </param>
    /// <param name="positionModel"> Модель редактируемой должности. </param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdatingPositionModel positionModel)
    {
        try
        {
            await _service.UpdateAsync(id, _mapper.Map<UpdatingPositionModel, UpdatingPositionDto>(positionModel));
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Удалить должность.
    /// </summary>
    /// <param name="id"> Идентификатор должности. </param>
    /// <returns></returns>
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
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить постраничный список должностей.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<IActionResult> GetListAsync(int page, int itemsPerPage)
    {
        try
        {
            return Ok(_mapper.Map<List<PositionModel>>(await _service.GetPagedAsync(page, itemsPerPage)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
