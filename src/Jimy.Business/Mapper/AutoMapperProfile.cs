﻿using AutoMapper;
using Jimy.Business.DTOs;
using Jimy.Data.Entities;

namespace Jimy.Business.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // ActivityLog mappings
        CreateMap<ActivityLog, ActivityLogDto>();
        CreateMap<CreateActivityLogDto, ActivityLog>();
        CreateMap<UpdateActivityLogDto, ActivityLog>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // User mappings
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // WorkoutPlan mappings
        CreateMap<WorkoutPlan, WorkoutPlanDto>();
        CreateMap<CreateWorkoutPlanDto, WorkoutPlan>();
        CreateMap<UpdateWorkoutPlanDto, WorkoutPlan>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Exercise mappings
        CreateMap<Exercise, ExerciseDto>();
        CreateMap<CreateExerciseDto, Exercise>();
        CreateMap<UpdateExerciseDto, Exercise>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}