using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportService.Data;
using ReportService.Entities;
using ReportService.Services;

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ReportDbContext _context;

        public ReportsController(ReportDbContext context)
        {
            _context = context;
        }

      
        [HttpPost("create")]
        public async Task<IActionResult> CreateReport([FromBody] string location)
        {
            var report = new Report
            {
                RequestedAt = DateTime.UtcNow,
                Location = location,
                Status = ReportStatus.Preparing,
                PersonCount = 0,
                PhoneNumberCount = 0 
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

           
            
            InMemoryReportQueue.Enqueue(report.Id);

            return Ok(new { report.Id, message = "Rapor talebi alındı ve işleniyor." });
        }

        // Raporları listeleme
        [HttpGet("getreports")]
        public async Task<IActionResult> GetReports()
        {
            var reports = await _context.Reports.ToListAsync();
            return Ok(reports);
        }

        // Rapor detayları
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetReportDetails(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
                return NotFound();

            return Ok(report);
        }
    }
}