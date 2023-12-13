using Agenda_Back.Data.Repository.Interfaces;
using Agenda_Back.Entities;
using Agenda_Back.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Agenda_Back.Controllers
{
    [Route("contactBook")]
    [ApiController]
    [Authorize]
    public class ContactBookController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IContactBookRepository _contactBookRepository;
        private readonly ISharedContactBookRepository _sharedContactBookRepository;

        public ContactBookController(IMapper mapper, IContactBookRepository contactBookRepository, ISharedContactBookRepository sharedContactBookRepository)
        {
            _mapper = mapper;
            _contactBookRepository = contactBookRepository;
            _sharedContactBookRepository = sharedContactBookRepository;
        }

        [HttpGet]
        public IActionResult GetAllContactBooks()
        {
            try
            {
                var contactBooks = _contactBookRepository.GetListContactBooks();
                var listContactBooks = _mapper.Map<IEnumerable<ContactBookDTO>>(contactBooks);
                return Ok(listContactBooks);

            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("getContactBookOfUser")]
        public IActionResult GetContactBook()
        {
            try
            {
                int userId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var listContactBookOfUser = _sharedContactBookRepository.GetSharedContactBooks(userId); //trae las contact book del usuario
                List<ContactBook> listContactBooks = new List<ContactBook>();

                foreach (var contactBookUser in listContactBookOfUser)
                {
                    var contactBook = _contactBookRepository.GetContactBookById(contactBookUser.ContactBookId);

                    listContactBooks.Add(contactBook);
                }

                var listContactBookDto = _mapper.Map<IEnumerable<ContactBookDTO>>(listContactBooks);
                return Ok(listContactBookDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost] //new contactBook
        public IActionResult CreateContactBook(ContactBookDTO contactBookDTO)
        {
            try
            {
                int userId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var contactBook = _mapper.Map<ContactBook>(contactBookDTO);

                var contactBookId = _contactBookRepository.CreateContactBook(contactBook); // devuelve el id del contactBook creado

                _sharedContactBookRepository.addSharedContactBook(userId, contactBookId);

                return Created("Created", contactBookId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteContactBook(int contactBookId)
        {
            try
            {
                int userId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var sharedContactBooks = _sharedContactBookRepository.GetSharedContactBooks(userId);

                List<ContactBook> contactBooksList = new List<ContactBook>();

                var contactBook = _contactBookRepository.GetContactBookById(contactBookId);

                foreach (var sharedContactBook in sharedContactBooks)
                {
                    if (sharedContactBook.ContactBookId == contactBookId)
                    {
                        if (contactBook == null)
                        {
                            return NotFound();
                        }

                        _sharedContactBookRepository.DeleteSharedContactBook(contactBookId);
                        _contactBookRepository.DeleteContactBook(contactBook);

                        return Ok("Ok");

                    }
                }
                return Unauthorized();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
