using AutoMapper;
using PodcastAPI.Application.DTOs.Auth;
using PodcastAPI.Application.DTOs.Episode;
using PodcastAPI.Application.DTOs.Podcast;
using PodcastAPI.Application.Features.Auth.Commands.Register;
using PodcastAPI.Application.Features.Podcasts.Commands.CreatePodcast;
using PodcastAPI.Application.Features.Podcasts.Queries.GetAllPodcasts;
using PodcastAPI.Application.Features.Podcasts.Queries.GetPodcastById;
using PodcastAPI.Application.Features.Podcasts.Queries.GetPodcastsBySearch;
using PodcastAPI.Application.Features.Subscriptions.Queries;
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


            CreateMap<CreatePodcast.Command, Podcast>();
            CreateMap<Podcast, CreatePodcast.Response>();

            CreateMap<Podcast, GetAllPodcast.Response>();

            CreateMap<Podcast, GetPodcastById.Response>();

            CreateMap<Register.Command, User>();
            CreateMap<Podcast, GetMySubscriptions.Response>();

            CreateMap<Podcast, GetPodcastsBySearch.Response>();
        }
    }
}
