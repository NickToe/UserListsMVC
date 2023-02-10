using AutoMapper;
using UserListsMVC.Json;

namespace UserListsAPI.DTOs;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        ValueTransformers.Add<string>(s => string.IsNullOrEmpty(s) ? String.Empty : s);

        CreateMap<GameJson, Game>();
        CreateMap<MovieJson, Movie>();
    }
}