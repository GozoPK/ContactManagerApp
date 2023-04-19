using AutoMapper;
using ContactManagerApi.DTOs;
using ContactManagerApi.Entities;
using ContactManagerApi.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly IMapper _mapper;

        public ContactsController(IContactsRepository contactsRepository, IEmailRepository emailRepository, IMapper mapper)
        {
            _contactsRepository = contactsRepository;
            _emailRepository = emailRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetContacts()
        {
            var contacts = await _contactsRepository.GetContacts();
            return Ok(_mapper.Map<IEnumerable<ContactDto>>(contacts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetContact(int id)
        {
            var contact = await _contactsRepository.GetContactById(id);
            if (contact == null) return NotFound();
            return Ok(_mapper.Map<ContactDto>(contact));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<ContactDto>> CreateContact(ContactDto contactForCreation)
        {
            var contactsList = await _contactsRepository.GetContacts() as List<Contact>;
            var contact = _mapper.Map<Contact>(contactForCreation);

            if (contactsList.Contains(contact)) return BadRequest("The contact already exists");

            if (await _contactsRepository.PhoneNumberExists(contactForCreation.MobileNumber)) return BadRequest("This Mobile number already exists");

            foreach (var email in contactForCreation.Emails)
            {
                if (await _emailRepository.EmailExists(email.EmailAddress)) return BadRequest("This email address already exists");
            }

            await _contactsRepository.AddContact(contact);
            if (await _contactsRepository.SaveAllAsync()) return Ok(_mapper.Map<ContactDto>(contact));

            return BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> UpdateContact(int id, ContactForUpdateDto contactDto)
        {
            var contact = await _contactsRepository.GetContactById(id);
            if (contact == null) return NotFound();
            
            _mapper.Map(contactDto, contact);
            
            if (!_contactsRepository.HasChanges()) return NoContent();
            if (await _contactsRepository.SaveAllAsync()) return Ok();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            await _contactsRepository.DeleteContact(id);

            if (await _contactsRepository.SaveAllAsync()) return Ok();

            return BadRequest();
        }

    }
}
