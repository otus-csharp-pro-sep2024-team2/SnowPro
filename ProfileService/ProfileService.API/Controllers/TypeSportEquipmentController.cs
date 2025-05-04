using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProfileService.API.Models.TypeSportEquipmentModels;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.TypeSportEquipmentContracts;

namespace ProfileService.API.Controllers;

[ApiController]
[EnableCors("AllowReactApp")]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class TypeSportEquipmentController : ControllerBase
{
    private readonly ITypeSportEquipmentServiceApp _service;
    private readonly IMapper _mapper;
    public TypeSportEquipmentController(ITypeSportEquipmentServiceApp service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Получить тип спортивного оборудования.
    /// </summary>
    /// <param name="id"> Идентификатор типа спортивного оборудования. </param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        return Ok(_mapper.Map<TypeSportEquipmentModel>(await _service.GetByIdAsync(id)));
    }

    /// <summary>
    /// Создать тип спортивного оборудования.
    /// </summary>
    /// <param name="typeSportEquipmentModel"> Модель создаваемого типа спортивного оборудования. </param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreatingTypeSportEquipmentModel typeSportEquipmentModel)
    {
        return Ok(await _service.CreateAsync(_mapper.Map<CreatingTypeSportEquipmentDto>(typeSportEquipmentModel)));
    }

    /// <summary>
    /// Изменить тип спортивного оборудования.
    /// </summary>
    /// <param name="id">Идентификатор типа спортивного оборудования.</param>
    /// <param name="typeSportEquipmentModel">Модель редактируемого типа спортивного оборудования.</param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdatingTypeSportEquipmentModel typeSportEquipmentModel)
    {
        try
        {
            await _service.UpdateAsync(id, _mapper.Map<UpdatingTypeSportEquipmentModel, UpdatingTypeSportEquipmentDto>(typeSportEquipmentModel));
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Удалить тип спортивного оборудования.
    /// </summary>
    /// <param name="id"> Идентификатор типа спортивного оборудования. </param>
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
            return Ok(_mapper.Map<List<TypeSportEquipmentModel>>(await _service.GetPagedAsync(page, itemsPerPage)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
