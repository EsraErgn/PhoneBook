using Microsoft.EntityFrameworkCore;
using ReportService.Entities;

namespace ReportService.Data
{
    public class ReportDbContext:DbContext
    {
        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options) { }

        public DbSet<Report> Reports { get; set; }
    }
}
