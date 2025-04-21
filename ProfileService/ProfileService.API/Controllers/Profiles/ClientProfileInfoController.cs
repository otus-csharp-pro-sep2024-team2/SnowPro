using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProfileService.API.Models.ClientProfileInfoModels;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.ClientProfileInfoContracts;
using SnowPro.Shared.Contracts;

namespace ProfileService.API.Controllers.Profiles;

[ApiController]
[EnableCors("AllowReactApp")]
[Route("/api/[controller]")]
[Authorize]
public class ClientProfileInfoController : ControllerBase
{
    private readonly IClientProfileInfoServiceApp _service;
    private readonly IMapper _mapper;
    private readonly ILogger<ClientProfileInfoController> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public ClientProfileInfoController(
        IClientProfileInfoServiceApp service,
        ILogger<ClientProfileInfoController> logger,
        IMapper mapper,
        IPublishEndpoint publishEndpoint
    )
    {
        _service = service;
        _logger = logger;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    /// <summary>
    /// Получить профиль клиента.
    /// </summary>
    /// <param name="id"> Идентификатор профиля клиента. </param>
    /// <returns></returns>
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return Ok(_mapper.Map<ClientProfileInfoModel>(await _service.GetByIdAsync(id)));
    }

    /// <summary>
    /// Получить профиль клиента по id пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetByUserIdAsync(Guid userId)
    {
        return Ok(_mapper.Map<ClientProfileInfoModel>(await _service.GetByUserIdAsync(userId)));
    }

    /// <summary>
    /// Создать профиль клиента
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="clientProfileModel"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(Guid userId, CreatingClientProfileInfoModel clientProfileModel)
    {
        return Ok(await _service.CreateAsync(userId, _mapper.Map<CreatingClientProfileInfoDto>(clientProfileModel)));
    }


    /// <summary>
    /// Изменить профиль клиента по Id пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="profileModel"> Модель редактируемого профиля клиента. </param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(Guid userId, UpdatingClientProfileInfoModel profileModel)
    {
        try
        {
            UpdatingClientProfileInfoDto profile = _mapper.Map<UpdatingClientProfileInfoModel, UpdatingClientProfileInfoDto>(profileModel);
            await _service.UpdateAsync(userId, profile);
            SharedProfileInfoDto sharedProfileInfoDto = _mapper.Map<UpdatingClientProfileInfoDto, SharedProfileInfoDto>(profile);
            sharedProfileInfoDto.UserId = userId;
            await _publishEndpoint.Publish(sharedProfileInfoDto);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Удалить профиль клиента по Id пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(Guid userId)
    {
        try
        {
            ClientProfileInfoDto profile = await _service.DeleteAsync(userId);
            SharedProfileInfoDto sharedProfileInfoDto = _mapper.Map<ClientProfileInfoDto, SharedProfileInfoDto>(profile);
            await _publishEndpoint.Publish(sharedProfileInfoDto);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить список профилей клиентов.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns></returns>
    [HttpGet("list")]
    public async Task<IActionResult> GetListAsync(int page, int itemsPerPage)
    {
        try
        {
            return Ok(_mapper.Map<List<ClientProfileInfoModel>>(await _service.GetPagedAsync(page, itemsPerPage)));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
