using AutoMapper;
using Gym.Domain.Entities;
using Gym.Services.DTO;

namespace Gym.Services.AutoMapperProfile;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        IndividualEntity();
        Workout();
        Professional();
        Exercise();
        ImageExercise();
        Login();
        User();
    }

    private void IndividualEntity()
        => CreateMap<IndividualEntity, IndividualEntityDTO>().ReverseMap();

    private void Workout()
        => CreateMap<Workout, WorkoutDTO>().ReverseMap();

    private void Professional()
        => CreateMap<Professional, ProfessionalDTO>().ReverseMap();

    private void Exercise()
        => CreateMap<Exercise, ExerciseDTO>().ReverseMap();

    private void ImageExercise()
    {
        CreateMap<string, byte[]>().ConvertUsing(str => Convert.FromBase64String(str));
        CreateMap<byte[], string>().ConvertUsing(bytes => Convert.ToBase64String(bytes));
        CreateMap<ImageExercise, ImageExerciseDTO>().ReverseMap();
    }

    private void Login()
    {
        CreateMap<Login, LoginDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Email.ToLower()))
            .ReverseMap();

        CreateMap<Login, LoginResetPasswordDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Email.ToLower()))
            .ReverseMap();
    }

    private void User()
        => CreateMap<User, UserDTO>()
            .ForMember(dest => dest.LoginId, opt => opt.MapFrom(src => src.Login.Id))
            .ForMember(dest => dest.IndividualEntityId, opt => opt.MapFrom(src => src.IndividualEntity.Id))
            .ForMember(dest => dest.IndividualEntity, opt => opt.MapFrom(src => src.IndividualEntity))
            .ReverseMap();
}
