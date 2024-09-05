using AutoMapper;
using Jimy.Business.DTOs;
using Jimy.Business.Interfaces;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Services;

public class ActivityLogService : IActivityLogService
{
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IMapper _mapper;

    public ActivityLogService(IActivityLogRepository activityLogRepository, IMapper mapper)
    {
        _activityLogRepository = activityLogRepository;
        _mapper = mapper;
    }

    public async Task<ActivityLogDto> GetActivityLogByIdAsync(int id)
    {
        var activityLog = await _activityLogRepository.GetByIdAsync(id);
        return _mapper.Map<ActivityLogDto>(activityLog);
    }

    public async Task<IEnumerable<ActivityLogDto>> GetActivityLogsByUserIdAsync(int userId)
    {
        var activityLogs = await _activityLogRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<ActivityLogDto>>(activityLogs);
    }

    public async Task<ActivityLogDto> CreateActivityLogAsync(CreateActivityLogDto createActivityLogDto)
    {
        var activityLog = _mapper.Map<ActivityLog>(createActivityLogDto);
        activityLog = await _activityLogRepository.AddAsync(activityLog);
        return _mapper.Map<ActivityLogDto>(activityLog);
    }

    public async Task<ActivityLogDto> UpdateActivityLogAsync(int id, UpdateActivityLogDto updateActivityLogDto)
    {
        var activityLog = await _activityLogRepository.GetByIdAsync(id);
        if (activityLog == null)
            throw new KeyNotFoundException("Activity log not found");

        _mapper.Map(updateActivityLogDto, activityLog);
        await _activityLogRepository.UpdateAsync(activityLog);
        return _mapper.Map<ActivityLogDto>(activityLog);
    }

    public async Task DeleteActivityLogAsync(int id)
    {
        await _activityLogRepository.DeleteAsync(id);
    }
}