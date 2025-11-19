using AutoMapper;
using PodcastAPI.Application.DTOs.Auth;
using PodcastAPI.Application.DTOs.Episode;
using PodcastAPI.Application.DTOs.Podcast;
using PodcastAPI.Domain.Entities;

namespace PodcastAPI.Application.MappingProfiles
{
    public class GeneralMappingProfile : Profile
    {
        public GeneralMappingProfile()
        {
            CreateMap<Podcast, PodcastDto>().ReverseMap();
            CreateMap<Episode, EpisodeDto>().ReverseMap();
            CreateMap<CreatePodcastRequest, Podcast>().ReverseMap();
            CreateMap<RegisterRequest, User>();
        }
    }
}
