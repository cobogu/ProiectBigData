using Microsoft.ML.Data;

namespace ProiectBigData.Models
{
    public class SymptomInput
    {

        [LoadColumn(2)]
        public string Text { get; set; }
    }
}
