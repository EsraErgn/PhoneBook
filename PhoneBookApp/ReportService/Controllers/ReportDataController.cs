using ContactService.Data;
using ContactService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ReportService.Controllers
{
    [Route("api/report-data")]
    [ApiController]
    public class ReportDataController : ControllerBase
    {

        private readonly PhoneBookDbContext _context;

        public ReportDataController(PhoneBookDbContext context)
        {
            _context = context;
        }

        [HttpGet("reports/{location}")]
        public IActionResult GetReportData(string location)
        {
            var personsAtLocation = _context.Persons
                .Include(p => p.ContactInfos)
                .Where(p => p.ContactInfos != null && p.ContactInfos
                    .Any(ci => ci.Type == ContactType.Location && ci.Content == location))
                .ToList();

            int personCount = personsAtLocation.Count;
            int phoneCount = personsAtLocation
                .SelectMany(p => p.ContactInfos)
                .Count(ci => ci.Type == ContactType.Phone);

            return Ok(new
            {
                PersonCount = personCount,
                PhoneCount = phoneCount
            });
        }
    }
}
