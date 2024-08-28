using AutoMapper;
using FantasyAssistant.Application.Model;
using FantasyAssistant.Transport;

namespace FantasyAssistant.AutoMapper;

public sealed class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<TeamDto, Team>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.name))
            .ForMember(dest => dest.Points, opts => opts.MapFrom(src => src.points))
            .ForMember(dest => dest.Strength, opts => opts.MapFrom(src => src.strength))
            .ReverseMap();
    }
}