using Microsoft.AspNetCore.Mvc;
using ProiectBigData.Services;
using ProiectBigData.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProiectBigData.Controllers
{
    public class DiseasePredictionController : Controller
    {
        private readonly IDiseasePredictionService _diseasePredictionService;

        public DiseasePredictionController(IDiseasePredictionService diseasePredictionService)
        {
            _diseasePredictionService = diseasePredictionService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new DiseasePredictionViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(DiseasePredictionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
                var input = new SymptomInput
                {
               
                    Text = model.Text
                };

            var prediction = await _diseasePredictionService.PredictDiseaseAsync(input);
           
            model.PredictedLabel = prediction;
           // model.Score = predictionResult.Score;

            return View(model);
        }
           
        }
    }

