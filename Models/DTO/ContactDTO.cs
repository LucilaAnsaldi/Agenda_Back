﻿using Agenda_Back.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Agenda_Back.Models.DTO
{
    public class ContactDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string? Description { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Location { get; set; }

        public int ContactBookId { get; set; }
    }
}
