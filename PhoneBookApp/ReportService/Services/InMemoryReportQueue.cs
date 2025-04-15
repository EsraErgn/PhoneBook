using System.Collections.Concurrent;

namespace ReportService.Services
{
    public static class InMemoryReportQueue
    {
        private static readonly ConcurrentQueue<Guid> _queue = new();

        public static void Enqueue(Guid reportId)
        {
            _queue.Enqueue(reportId);
        }

        public static bool TryDequeue(out Guid reportId)
        {
            return _queue.TryDequeue(out reportId);
        }
    }
}
