using AutoMapper;
using FakeFootball.Dtos;
using FakeFootball.Models;

namespace FakeFootball.Profiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<TeamModel, TeamsReadDto>();
            CreateMap<ScoresModel, ScoresReadDto>();
            CreateMap<TeamCreateDto, TeamModel>()
                .ForMember(dest => dest.Points, opts => opts.MapFrom(src => 0))
                .ForMember(dest => dest.GamesPlayed, opts => opts.MapFrom(opts => 0));
            CreateMap<TeamUpdateDto, TeamModel>().ReverseMap();
        }
    }
}
