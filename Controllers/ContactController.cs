using Agenda_Back.Data.Repository.Interfaces;
using Agenda_Back.Entities;
using Agenda_Back.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Agenda_Back.Controllers
{
    [Route("contact")]
    [ApiController]
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly IContactBookRepository _contactBookRepository;
        private readonly ISharedContactBookRepository _sharedContactBookRepository;
        private readonly IMapper _mapper;

        public ContactController(IContactRepository contactRepository, IContactBookRepository contactBookRepository, ISharedContactBookRepository sharedContactBookRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _contactBookRepository = contactBookRepository;
            _sharedContactBookRepository = sharedContactBookRepository;
            _mapper = mapper;
        }

        [HttpGet("contactBook/{contactBookId}")]
        public IActionResult GetContactBook(int contactBookId)
        {
            try
            {
                var contactsList = _contactRepository.GetListContacts(contactBookId);
                var contactsListDto = _mapper.Map<IEnumerable<ContactDTO>>(contactsList);
                return Ok(contactsListDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            try
            {
                var contact = _contactRepository.GetContact(id);

                if (contact == null)
                {
                    return NotFound();
                }

                var contactDto = _mapper.Map<ContactDTO>(contact);
                return Ok(contactDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] // new Contact
        public IActionResult CreateContact(ContactDTO contactDto) 
        {
            try
            { 
                int userID = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var contact = _mapper.Map<Contact>(contactDto);

                var listContactBook = _sharedContactBookRepository.GetSharedContactBooks(userID);

                foreach (var contactBookId in listContactBook)
                {
                    if (contact.ContactBookId == contactBookId.ContactBookId)
                    {
                        var contactCreated = _contactRepository.AddContact(contact);
                        var contactDtoToReturn = _mapper.Map<ContactDTO>(contactCreated);
                        return Created("Created", contactDtoToReturn);
                    }
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteContact(int contactId)
        {
            try
            {
                int userID = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var sharedContactBookList = _sharedContactBookRepository.GetSharedContactBooks(userID);

                var contact = _contactRepository.GetContact(contactId);

                if (contact == null)
                {
                    return NotFound();
                }

                foreach (var sharedContactBook in sharedContactBookList)
                {
                    if(sharedContactBook.ContactBookId == contact.ContactBookId)
                    {
                        _contactRepository.DeleteContact(contact);
                        return NoContent();
                    }
                }

                return Unauthorized();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
