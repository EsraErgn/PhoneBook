using ContactService.Data;
using ContactService.DTOs.Person;
using ContactService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Services
{
    public class PersonService : IPersonService
    {
        private readonly PhoneBookDbContext _context;

        public PersonService(PhoneBookDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(CreatePersonRequest request)
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Company = request.Company
            };

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return person.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null) return false;

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PersonResponse>> GetAllAsync()
        {
            return await _context.Persons
                .Select(p => new PersonResponse
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Company = p.Company
                })
                .ToListAsync();
        }

        public async Task<PersonResponse> GetByIdAsync(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null) return null;

            return new PersonResponse
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Company = person.Company
            };
        }
    }
}
