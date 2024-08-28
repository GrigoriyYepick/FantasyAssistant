using AutoMapper;
using FantasyAssistant.Application.Model;
using FantasyAssistant.Transport;

namespace FantasyAssistant.AutoMapper;

public sealed class FixtureProfile : Profile
{
    public FixtureProfile()
    {
        CreateMap<FixtureDto, Fixture>()
            .ForMember(dest => dest.EventId, opts => opts.MapFrom(src => src.id))
            .ForMember(dest => dest.TeamAway, opts => opts.MapFrom(src => src.team_a))
            .ForMember(dest => dest.TeamHome, opts => opts.MapFrom(src => src.team_h))
            .ForMember(dest => dest.IsFinished, opts => opts.MapFrom(src => src.finished))
            .ReverseMap();
    }
}