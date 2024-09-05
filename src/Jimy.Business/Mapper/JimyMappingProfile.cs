using AutoMapper;
using Jimy.Business.DTOs;
using Jimy.Data.Entities;

namespace Jimy.Business.Mapper;

public class JimyMappingProfile : Profile
{
    public JimyMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();

        CreateMap<WorkoutPlan, WorkoutPlanDto>();
        CreateMap<CreateWorkoutPlanDto, WorkoutPlan>();
        CreateMap<UpdateWorkoutPlanDto, WorkoutPlan>();

        CreateMap<Exercise, ExerciseDto>();
        CreateMap<CreateExerciseDto, Exercise>();
        CreateMap<UpdateExerciseDto, Exercise>();

        CreateMap<WorkoutExercise, WorkoutExerciseDto>();
        CreateMap<CreateWorkoutExerciseDto, WorkoutExercise>();
        CreateMap<UpdateWorkoutExerciseDto, WorkoutExercise>();

        CreateMap<ActivityLog, ActivityLogDto>();
        CreateMap<CreateActivityLogDto, ActivityLog>();
        CreateMap<UpdateActivityLogDto, ActivityLog>();
    }
}