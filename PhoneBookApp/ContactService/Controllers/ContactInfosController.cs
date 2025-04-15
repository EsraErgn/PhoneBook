using ContactService.Data;
using ContactService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfosController : ControllerBase
    {
        private readonly PhoneBookDbContext _context;

        public ContactInfosController(PhoneBookDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddContactInfo([FromBody] ContactInfo info)
        {
            _context.ContactInfos.Add(info);
            await _context.SaveChangesAsync();
            return Ok(info);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactInfo(Guid id)
        {
            var info = await _context.ContactInfos.FindAsync(id);
            if (info == null) return NotFound();

            _context.ContactInfos.Remove(info);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("person/{personId}")]
        public async Task<IActionResult> GetContactInfosByPerson(Guid personId)
        {
            var infos = await _context.ContactInfos
                .Where(c => c.PersonId == personId)
                .ToListAsync();

            return Ok(infos);
        }
    }
}
