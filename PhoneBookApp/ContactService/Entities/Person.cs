using System.ComponentModel.DataAnnotations;

namespace ContactService.Entities
{
    public class Person
    {
        
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Company { get; set; }

        public ICollection<ContactInfo> ContactInfos { get; set; }
    }
}
