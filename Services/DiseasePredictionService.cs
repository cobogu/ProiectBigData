using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ProiectBigData.Models;
using System.Linq;

namespace ProiectBigData.Services
{
    public class DiseasePredictionService : IDiseasePredictionService
    {
        private readonly HttpClient _httpClient;
        public DiseasePredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> PredictDiseaseAsync(SymptomInput input)
        {


            var response = await _httpClient.PostAsJsonAsync("/predict", input);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<DiseaseApiResponse>();
            return result?.PredictedLabel;

        }
            private class DiseaseApiResponse
        {
            public string PredictedLabel { get; set; }
        }
    }
}

