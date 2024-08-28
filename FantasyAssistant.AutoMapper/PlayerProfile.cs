using AutoMapper;
using FantasyAssistant.Application.Model;
using FantasyAssistant.Transport;

namespace FantasyAssistant.AutoMapper;

public sealed class PlayerProfile : Profile
{
    public PlayerProfile()
    {
        CreateMap<PlayerDto, Player>()
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.web_name))
            .ForMember(dest => dest.Type, opts => opts.MapFrom(source => Enum.GetName(typeof(PlayerType), source.element_type)))
            .ForMember(dest => dest.TeamId, opts => opts.MapFrom(src => src.team))
            .ForMember(dest => dest.Cost, opts => opts.MapFrom(src => src.now_cost))
            .ForMember(dest => dest.Points, opts => opts.MapFrom(src => src.total_points))
            .ForMember(dest => dest.Form, opts => opts.MapFrom(src => src.value_form))
            .ForMember(dest => dest.ChanceOfPlaying, opts => opts.MapFrom(src => src.chance_of_playing_next_round))
            .ForMember(dest => dest.Team, opts => opts.Ignore())
            .ReverseMap();
    }
}