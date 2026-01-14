using System.Threading.Tasks;
using ProiectBigData.Models;

namespace ProiectBigData.Services
{
    public interface IDiseasePredictionService
    {
        Task<string> PredictDiseaseAsync(SymptomInput input);
    }
}
