using AutoMapper;
using EventHubTicket.Management.Application.Dtos;
using EventHubTicket.Management.Application.ViewModels;
using EventHubTicket.Management.Domain.Entities;

namespace EventHubTicket.Management.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventListViewModel>()
                .ForMember(dest =>
                    dest.EventId,
                    opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Event, EventDetailViewModel>()
                .ForMember(dest =>
                    dest.EventId,
                    opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Category, CategoryDto>();
        }
    }
}
