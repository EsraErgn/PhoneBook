
using ReportService.Entities;
using ReportService.Data;

namespace ReportService.Services
{
    public class ReportBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ReportBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (InMemoryReportQueue.TryDequeue(out Guid reportId))
                {
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<ReportDbContext>();

                    var report = await context.Reports.FindAsync(reportId);
                    if (report != null)
                    {
                        
                        report.PersonCount = new Random().Next(1, 10); 
                        report.PhoneNumberCount = new Random().Next(1, 20);

                        report.Status = ReportStatus.Completed;
                        await context.SaveChangesAsync();
                    }
                }

                await Task.Delay(5000, stoppingToken); 
            }
        }
    }
}

