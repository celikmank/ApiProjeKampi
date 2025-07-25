using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.ContactDtos;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactMessagesController : ControllerBase
    {
        private readonly ApiContext _context;
        public ContactMessagesController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateMessage(CreateContactMessageDto dto)
        {
            var message = new ContactMessage
            {
                Name = dto.Name,
                Email = dto.Email,
                Subject = dto.Subject,
                Message = dto.Message,
                CreatedAt = DateTime.Now
            };

            _context.ContactMessages.Add(message);
            _context.SaveChanges();
            return Ok("Mesaj başarıyla alındı");
        }

        [HttpGet]
        public IActionResult GetMessages()
        {
            var values = _context.ContactMessages.ToList();
            return Ok(values);
        }
    }
}
