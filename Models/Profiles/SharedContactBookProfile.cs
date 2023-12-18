using Agenda_Back.Entities;
using Agenda_Back.Models.DTO;
using AutoMapper;

namespace Agenda_Back.Models.Profiles
{
    public class SharedContactBookProfile : Profile
    {
        public SharedContactBookProfile() 
        { 
            CreateMap<SharedContactBook, SharedContactBookDTO>();
            CreateMap<SharedContactBookDTO, SharedContactBook>();
        }
    }
}
