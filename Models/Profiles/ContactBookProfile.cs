﻿using Agenda_Back.Entities;
using Agenda_Back.Models.DTO;
using AutoMapper;

namespace Agenda_Back.Models.Profiles
{
    public class ContactBookProfile : Profile
    {
        public ContactBookProfile() {
            CreateMap<ContactBook, ContactBookDTO>();
            CreateMap<ContactBookDTO, ContactBook>();
        }
    }
}
