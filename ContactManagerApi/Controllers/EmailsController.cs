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
    [Route("api/{id}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IContactsRepository _contactsRepository;
        private readonly IMapper _mapper;

        public EmailsController(IEmailRepository emailRepository, IContactsRepository contactsRepository, IMapper mapper)
        {
            _emailRepository = emailRepository;
            _contactsRepository = contactsRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<EmailDto>> CreateEmail(EmailDto emailDto, int id)
        {
            if (await _emailRepository.EmailExists(emailDto.EmailAddress)) return BadRequest("This email address already exists");

            var contact = await _contactsRepository.GetContactById(id);
            var email = _mapper.Map<Email>(emailDto);
            contact.Emails.Add(email);

            if (await _emailRepository.SaveAllAsync()) return Ok(emailDto);

            return BadRequest();
        }

        [HttpDelete("{emailAddress}")]
        public async Task<ActionResult<EmailDto>> DeleteEmail(string emailAddress, int id)
        {
            var email = await _emailRepository.GetEmail(emailAddress);
            if (email == null) return NotFound();

            var contact = await _contactsRepository.GetContactById(id);
            contact.Emails.Remove(email);

            if (await _emailRepository.SaveAllAsync()) return Ok();

            return BadRequest();
        }
    }
}
