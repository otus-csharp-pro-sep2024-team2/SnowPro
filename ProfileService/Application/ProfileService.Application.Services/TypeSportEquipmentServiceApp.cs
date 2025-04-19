using AutoMapper;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.TypeSportEquipmentContracts;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Services;

public class TypeSportEquipmentServiceApp : ITypeSportEquipmentServiceApp
{
    private readonly IMapper _mapper;
    private readonly ITypeSportEquipmentRepository _typeSportEquipmentRepository;

    public TypeSportEquipmentServiceApp(
            IMapper mapper,
            ITypeSportEquipmentRepository typeSportEquipmentRepository
        )
    {
        _mapper = mapper;
        _typeSportEquipmentRepository = typeSportEquipmentRepository;
    }

    /// <summary>
    /// Получить тип спортивного оборудования.
    /// </summary>
    /// <param name="id"> Идентификатор типа спортивного оборудования. </param>
    /// <returns> ДТО типа спортивного оборудования. </returns>
    public async Task<TypeSportEquipmentDto> GetByIdAsync(int id)
    {
        var typeSportEquipment = await _typeSportEquipmentRepository.GetAsync(id, CancellationToken.None);
        return _mapper.Map<TypeSportEquipment, TypeSportEquipmentDto>(typeSportEquipment);
    }

    /// <summary>
    /// Создать тип спортивного оборудования.
    /// </summary>
    /// <param name="creatingTypeSportEquipmentDto"> ДТО создаваемого типа спортивного оборудования. </param>
    public async Task<int> CreateAsync(CreatingTypeSportEquipmentDto creatingTypeSportEquipmentDto)
    {
        var typeSportEquipment = _mapper.Map<CreatingTypeSportEquipmentDto, TypeSportEquipment>(creatingTypeSportEquipmentDto);
        var createdTypeSportEquipment = await _typeSportEquipmentRepository.AddAsync(typeSportEquipment);
        await _typeSportEquipmentRepository.SaveChangesAsync();
        return createdTypeSportEquipment.Id;
    }

    /// <summary>
    /// Изменить тип спортивного оборудования.
    /// </summary>
    /// <param name="id"> Идентификатор типа спортивного оборудования. </param>
    /// <param name="updatingTypeSportEquipmentDto"> ДТО редактируемого типа спортивного оборудования. </param>
    public async Task UpdateAsync(int id, UpdatingTypeSportEquipmentDto updatingTypeSportEquipmentDto)
    {
        var typeSportEquipment = await _typeSportEquipmentRepository.GetAsync(id, CancellationToken.None);
        if (typeSportEquipment == null)
        {
            throw new Exception($"Тип спортивного оборудования с идентфикатором {id} не найден");
        }

        typeSportEquipment.Name = updatingTypeSportEquipmentDto.Name;

        _typeSportEquipmentRepository.Update(typeSportEquipment);
        await _typeSportEquipmentRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить тип спортивного оборудования.
    /// </summary>
    /// <param name="id"> Идентификатор типа спортивного оборудования. </param>
    public async Task DeleteAsync(int id)
    {
        var typeSportEquipment = await _typeSportEquipmentRepository.GetAsync(id, CancellationToken.None);
        if (typeSportEquipment == null)
        {
            throw new Exception($"Тип спортивного оборудования с идентфикатором {id} не найден");
        }

        _typeSportEquipmentRepository.Delete(typeSportEquipment);
        await _typeSportEquipmentRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Получить постраничный список.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Страница. </returns>
    public async Task<ICollection<TypeSportEquipmentDto>> GetPagedAsync(int page, int itemsPerPage)
    {
        if (page <= 0)
        {
            throw new ArgumentException("Номер страницы должен быть больше 0", nameof(page));
        }

        if (itemsPerPage <= 0)
        {
            throw new ArgumentException("Количество элементов на странице должно быть больше 0", nameof(itemsPerPage));
        }

        ICollection<TypeSportEquipment> entities = await _typeSportEquipmentRepository.GetPagedAsync(page, itemsPerPage);
        return _mapper.Map<ICollection<TypeSportEquipment>, ICollection<TypeSportEquipmentDto>>(entities);
    }
}
