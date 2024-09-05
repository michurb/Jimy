using AutoMapper;
using Jimy.Business.DTOs;
using Jimy.Business.Interfaces;
using Jimy.Data.Entities;
using Jimy.Data.Interfaces;

namespace Jimy.Business.Services;

public class WorkoutPlanService : IWorkoutPlanService
{
    private readonly IWorkoutPlanRepository _workoutPlanRepository;
    private readonly IWorkoutExerciseRepository _workoutExerciseRepository;
    private readonly IMapper _mapper;

    public WorkoutPlanService(IWorkoutPlanRepository workoutPlanRepository, IWorkoutExerciseRepository workoutExerciseRepository, IMapper mapper)
    {
        _workoutPlanRepository = workoutPlanRepository;
        _workoutExerciseRepository = workoutExerciseRepository;
        _mapper = mapper;
    }

    public async Task<WorkoutPlanDto> GetWorkoutPlanByIdAsync(int id)
    {
        var workoutPlan = await _workoutPlanRepository.GetByIdAsync(id);
        return _mapper.Map<WorkoutPlanDto>(workoutPlan);
    }

    public async Task<IEnumerable<WorkoutPlanDto>> GetWorkoutPlansByUserIdAsync(int userId)
    {
        var workoutPlans = await _workoutPlanRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<WorkoutPlanDto>>(workoutPlans);
    }

    public async Task<WorkoutPlanDto> CreateWorkoutPlanAsync(CreateWorkoutPlanDto createWorkoutPlanDto)
    {
        var workoutPlan = _mapper.Map<WorkoutPlan>(createWorkoutPlanDto);
        workoutPlan = await _workoutPlanRepository.AddAsync(workoutPlan);
        return _mapper.Map<WorkoutPlanDto>(workoutPlan);
    }

    public async Task<WorkoutPlanDto> UpdateWorkoutPlanAsync(int id, UpdateWorkoutPlanDto updateWorkoutPlanDto)
    {
        var workoutPlan = await _workoutPlanRepository.GetByIdAsync(id);
        if (workoutPlan == null)
            throw new KeyNotFoundException("Workout plan not found");

        _mapper.Map(updateWorkoutPlanDto, workoutPlan);
        await _workoutPlanRepository.UpdateAsync(workoutPlan);
        return _mapper.Map<WorkoutPlanDto>(workoutPlan);
    }

    public async Task DeleteWorkoutPlanAsync(int id)
    {
        await _workoutPlanRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<WorkoutExerciseDto>> GetWorkoutExercisesAsync(int workoutPlanId)
    {
        var workoutExercises = await _workoutExerciseRepository.GetByWorkoutPlanIdAsync(workoutPlanId);
        return _mapper.Map<IEnumerable<WorkoutExerciseDto>>(workoutExercises);
    }

    public async Task<WorkoutExerciseDto> AddExerciseToWorkoutPlanAsync(int workoutPlanId, CreateWorkoutExerciseDto createWorkoutExerciseDto)
    {
        var workoutExercise = _mapper.Map<WorkoutExercise>(createWorkoutExerciseDto);
        workoutExercise.WorkoutPlanId = workoutPlanId;
        workoutExercise = await _workoutExerciseRepository.AddAsync(workoutExercise);
        return _mapper.Map<WorkoutExerciseDto>(workoutExercise);
    }

    public async Task RemoveExerciseFromWorkoutPlanAsync(int workoutPlanId, int exerciseId)
    {
        var workoutExercise = await _workoutExerciseRepository.GetByWorkoutPlanAndExerciseIdAsync(workoutPlanId, exerciseId);
        if (workoutExercise != null)
        {
            await _workoutExerciseRepository.DeleteAsync(workoutExercise.Id);
        }
    }
}