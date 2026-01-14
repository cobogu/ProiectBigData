using Grpc.Core;
using ProiectBigData.GrpcService; // Namespace-ul generat din proto

namespace ProiectBigData.GrpcService.Services
{
    // Moștenim clasa de bază generată automat din fișierul .proto
    public class AnalyticsService : AnalyticsMonitor.AnalyticsMonitorBase
    {
        private readonly ILogger<AnalyticsService> _logger;

        public AnalyticsService(ILogger<AnalyticsService> logger)
        {
            _logger = logger;
        }

        // Suprascriem metoda definită în .proto
        public override Task<StatusReply> GetSystemStatus(StatusRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Am primit o cerere de status de la: {request.ComponentName}");

            // Construim răspunsul
            var reply = new StatusReply
            {
                Message = $"Salut {request.ComponentName}, sistemul BigData funcționează perfect!",
                IsOperational = true,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            return Task.FromResult(reply);
        }
    }
}