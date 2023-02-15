using AutoMapper;
using UserListsMVC.Application.DTOs;
using UserListsMVC.Infrastructure.Api.Json;

namespace UserListsMVC.Infrastructure;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        ValueTransformers.Add<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);

        CreateMap<GameJson, GameDTO>();
        CreateMap<MovieJson, MovieDTO>();
    }
}