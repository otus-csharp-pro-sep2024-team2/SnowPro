using AutoMapper;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.TypeSportEquipmentContracts;
using ProfileService.Application.Contracts.TypeSportEquipmentProfileInfoContracts;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Services;

public class TypeSportEquipmentProfileServiceApp: ITypeSportEquipmentProfileServiceApp
{
    private readonly IMapper _mapper;
    private readonly ITypeSportEquipmentRepository _typeSportEquipmentRepository;
    private readonly ILevelTrainingRepository _levelTrainingRepository;

    public TypeSportEquipmentProfileServiceApp(
            IMapper mapper,
            ITypeSportEquipmentRepository typeSportEquipmentRepository,
            ILevelTrainingRepository levelTrainingRepository
        )
    {
        _mapper = mapper;
        _typeSportEquipmentRepository = typeSportEquipmentRepository;
        _levelTrainingRepository = levelTrainingRepository;
    }

    /// <summary>
    /// Создать спортивное оборудование.
    /// </summary>
    /// <param name="creatingSportEquipmentDto"> ДТО создаваемого спортивного оборудования. </param>
    public async Task<TypeSportEquipmentProfile> CreateAsync(CreatingTypeSportEquipmentProfileInfoDto creatingSportEquipmentDto)
    {
        TypeSportEquipmentProfile sportEquipment = _mapper.Map<CreatingTypeSportEquipmentProfileInfoDto, TypeSportEquipmentProfile>(creatingSportEquipmentDto);

        if (creatingSportEquipmentDto.TypeSportEquipmentName != null)
        {
            sportEquipment.TypeSportEquipment = await _typeSportEquipmentRepository.GetByNameAsync(creatingSportEquipmentDto.TypeSportEquipmentName, CancellationToken.None);
        }

        if(creatingSportEquipmentDto.LevelTrainingName != null)
        {
            sportEquipment.LevelTraining = await _levelTrainingRepository.GetByNameAsync(creatingSportEquipmentDto.LevelTrainingName, CancellationToken.None);
        }
        
        return sportEquipment;
    }
}