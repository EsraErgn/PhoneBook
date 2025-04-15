using ContactService.Data;
using ContactService.DTOs.Person;
using ContactService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController:ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly PhoneBookDbContext _context;

        public PersonsController(IPersonService personService, PhoneBookDbContext context)
        {
            _personService = personService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _personService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var person = await _personService.GetByIdAsync(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonRequest request)
        {
            var id = await _personService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _personService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetPersonDetails(Guid id)
        {
            var person = await _context.Persons
                .Include(p => p.ContactInfos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }
    }
}
