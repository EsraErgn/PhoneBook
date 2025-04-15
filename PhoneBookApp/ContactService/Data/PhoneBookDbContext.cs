using ContactService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ContactService.Data
{
    public class PhoneBookDbContext:DbContext
    {
        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options)
        : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
    }
}
