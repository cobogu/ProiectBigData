using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectBigData.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string Nume { get; set; }

        public int Varsta { get; set; }

        // --- Relația cu Specializarea ---
        [ForeignKey("Specializare")]
        public int SpecializareId { get; set; }
        public virtual Specializare? Specializare { get; set; }

        // --- Relația cu Spitalul ---
        [ForeignKey("Spital")]
        public int SpitalId { get; set; }
        public virtual Spital? Spital { get; set; }

    }
}
