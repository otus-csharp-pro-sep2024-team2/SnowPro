using Microsoft.EntityFrameworkCore;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Domain.Entities;

namespace ProfileService.Infrastructure.Repositories.Implementations;

public class TypeSportEquipmentProfileRepository : Repository<TypeSportEquipmentProfile, int>, ITypeSportEquipmentProfileRepository
{
    public TypeSportEquipmentProfileRepository(DbContext context) : base(context)
    {
    }

}
