using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProfileService.API.Models.ProfileInfoModels;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.ProfileInfoContracts;
using ProfileService.Domain.Entities;
using SnowPro.Shared.Contracts;


namespace ProfileService.API.Controllers.Profiles;

[ApiController]
[EnableCors("AllowReactApp")]
[Route("/api/[controller]")]
[Authorize]
public class ProfileInfoController : ControllerBase
{
    private readonly IProfileInfoServiceApp _service;
    private readonly IMapper _mapper;
    private readonly ILogger<ProfileInfoController> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    public ProfileInfoController(
        IProfileInfoServiceApp service,
        ILogger<ProfileInfoController> logger,
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
    /// Получить профиль.
    /// </summary>
    /// <param name="id"> Идентификатор профиля. </param>
    /// <returns></returns>
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return Ok(_mapper.Map<ProfileInfoModel>(await _service.GetByIdAsync(id)));
    }

    /// <summary>
    /// Получить профиль пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetByUserIdAsync(Guid userId)
    {
        return Ok(_mapper.Map<ProfileInfoModel>(await _service.GetByUserIdAsync(userId)));
    }

    /// <summary>
    /// Создать профиль пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="profileModel"> Модель создаваемого профиля. </param>
    [HttpPost]
    [Authorize(Roles = "Admin, Instructor")]
    public async Task<IActionResult> CreateAsync(Guid userId, CreatingProfileInfoModel profileModel)
    {
        try
        {
            Guid profileId = await _service.CreateAsync(userId, _mapper.Map<CreatingProfileInfoDto>(profileModel));
            SharedProfileInfoDto sharedProfileInfoDto = _mapper.Map<CreatingProfileInfoModel, SharedProfileInfoDto>(profileModel);
            await _publishEndpoint.Publish(sharedProfileInfoDto);
            return Ok(profileId);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Изменить профиль.
    /// </summary>
    /// <param name="id"> Идентификатор профиля. </param>
    /// <param name="profileModel"> Модель редактируемого профиля. </param>
    /// <returns></returns>
    [HttpPut("{id:Guid}")]
    [Authorize(Roles = "Admin, Instructor")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdatingProfileInfoModel profileModel)
    {
        try
        {
            var profile = _mapper.Map<UpdatingProfileInfoModel, UpdatingProfileInfoDto>(profileModel);
            await _service.UpdateAsync(id, profile);
            ProfileInfo profileInfo = _mapper.Map<UpdatingProfileInfoDto, ProfileInfo>(profile);
            SharedProfileInfoDto sharedProfileInfoDto = _mapper.Map<ProfileInfo, SharedProfileInfoDto>(profileInfo);
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
    /// Удалить профиль.
    /// </summary>
    /// <param name="id"> Идентификатор профиля. </param>
    /// <returns></returns>
    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin, Instructor")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            ProfileInfoDto profileInfoDto = await _service.DeleteAsync(id);
            SharedProfileInfoDto sharedProfileInfoDto = _mapper.Map<ProfileInfoDto, SharedProfileInfoDto>(profileInfoDto);
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
    /// Получить постраничный список профилей.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns></returns>
    [HttpGet("list")]
    [Authorize(Roles = "Admin, Instructor")]
    public async Task<IActionResult> GetListAsync(int page, int itemsPerPage)
    {
        try
        {
            return Ok(_mapper.Map<List<ProfileInfoModel>>(await _service.GetPagedAsync(page, itemsPerPage)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
