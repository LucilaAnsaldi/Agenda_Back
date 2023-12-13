using Agenda_Back.Entities;
using Agenda_Back.Models.DTO;
using AutoMapper;

namespace Agenda_Back.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<User, UserDTOCreation>();
            CreateMap<UserDTOCreation, User>();
        }
    }
}
