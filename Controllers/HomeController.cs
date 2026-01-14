using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProiectBigData.Models;
using ProiectBigData.GrpcService;

namespace ProiectBigData.Controllers
{
    public class HomeController : Controller
    {
        // Injectăm clientul gRPC
        private readonly AnalyticsMonitor.AnalyticsMonitorClient _grpcClient;

        public HomeController(AnalyticsMonitor.AnalyticsMonitorClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        public async Task<IActionResult> Index()
        {
            // ... cod existent ...

            try
            {
                // Apelăm serviciul gRPC
                var request = new StatusRequest { ComponentName = "MVC Web App" };
                var reply = await _grpcClient.GetSystemStatusAsync(request);

                // Trimitem datele în View prin ViewBag
                ViewBag.GrpcMessage = reply.Message;
                ViewBag.GrpcTime = reply.Timestamp;
            }
            catch (Exception ex)
            {
                ViewBag.GrpcMessage = "Serverul gRPC nu răspunde.";
            }

            return View();
        }
    }
}
