using System.ComponentModel.DataAnnotations;

namespace ProiectBigData.Models
{
    public class DiseasePredictionViewModel
    {

        [Required(ErrorMessage = "Te rog descrie simptomele.")]
        public string Text { get; set; }

        public string? PredictedLabel { get; set; }


    }
}
