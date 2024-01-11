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

            CreateMap<Category, CategoryListViewModel>()
                .ForMember(dest =>
                    dest.CategoryId,
                    opt => opt.MapFrom(src => src.Id));

            CreateMap<Category, CategoryEventListViewModel>()
                .ForMember(dest =>
                    dest.CategoryId,
                    opt => opt.MapFrom(src => src.Id));
        }
    }
}
