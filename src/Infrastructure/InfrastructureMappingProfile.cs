using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Api.Json;

namespace Infrastructure;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        ValueTransformers.Add<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);

        CreateMap<GameJson, GameDTO>();
        CreateMap<MovieJson, MovieDTO>();

        CreateMap<CustomListItemDTO, CustomListItem>();
        CreateMap<FollowlistItemDTO, FollowlistItem>();
        CreateMap<WishlistItemDTO, WishlistItem>();
    }
}