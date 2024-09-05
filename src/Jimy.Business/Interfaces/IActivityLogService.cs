using Jimy.Business.DTOs;

namespace Jimy.Business.Interfaces;

public interface IActivityLogService
{
    Task<ActivityLogDto> GetActivityLogByIdAsync(int id);
    Task<IEnumerable<ActivityLogDto>> GetActivityLogsByUserIdAsync(int userId);
    Task<ActivityLogDto> CreateActivityLogAsync(CreateActivityLogDto createActivityLogDto);
    Task<ActivityLogDto> UpdateActivityLogAsync(int id, UpdateActivityLogDto updateActivityLogDto);
    Task DeleteActivityLogAsync(int id);
}