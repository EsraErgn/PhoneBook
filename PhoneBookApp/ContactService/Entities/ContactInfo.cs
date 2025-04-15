using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContactService.Entities
{
    public enum ContactType
    {
        Phone = 1,
        Email = 2,
        Location = 3
    }
    public class ContactInfo
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public ContactType Type { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [ForeignKey("PersonId")]
        public Person Person { get; set; }
    }
}
