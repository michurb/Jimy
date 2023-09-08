using AutoMapper;
using Jimy.Api.DTO;
using Jimy.Api.Entities;

namespace Jimy.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Exercise, ExerciseDto>();
        CreateMap<ExerciseDetails, ExerciseDetailsDto>()
            .ForMember(dest => dest.Name,
                opt
                    => opt.MapFrom(src => src.Exercise.Name));
        CreateMap<TrainingSession, TrainingSessionDto>();
        CreateMap<TrainingSessionInputDto, TrainingSession>();
    }
}