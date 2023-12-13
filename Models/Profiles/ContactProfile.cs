using Agenda_Back.Entities;
using Agenda_Back.Models.DTO;
using AutoMapper;

namespace Agenda_Back.Models.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile() 
        {
            CreateMap<Contact, ContactDTO>();
            CreateMap<ContactDTO, Contact>();
        }
    }
}
