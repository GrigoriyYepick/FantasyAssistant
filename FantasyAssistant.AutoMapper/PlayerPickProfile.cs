using AutoMapper;
using FantasyAssistant.Application.Model;
using FantasyAssistant.Transport;

namespace FantasyAssistant.AutoMapper;

internal sealed class PlayerPickProfile : Profile
{
    public PlayerPickProfile()
    {
        CreateMap<PickDto, PlayerPick>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.element))
            .ForMember(dest => dest.IsCaptain, opts => opts.MapFrom(src => src.is_captain))
            .ForMember(dest => dest.IsViceCaptain, opts => opts.MapFrom(src => src.is_vice_captain))
            .ReverseMap();
    }
}