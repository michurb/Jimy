using Jimy.Application.DTO;
using Jimy.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jimy.Infrastructure.DAL.Handlers;

public static class Extensions
{
    public static UserDto AsDto(this User entity)
        => new(entity.Id.Value, entity.Username.Value, entity.Username.Value, entity.CreatedAt);

    public static ExerciseDto AsDto(this Exercise entity)
        => new(entity.Id.Value, entity.Name.Value, entity.Description.Value);
}