using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectBigData.Models
{
    public class StireMedicala
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Titlul este obligatoriu")]
        public string Titlu { get; set; }

        [Required(ErrorMessage = "Descrierea intervenției este obligatorie")]
        public string Descriere { get; set; }

        public DateTime DataPublicarii { get; set; } = DateTime.Now; // Se pune automat data curentă

        // --- Relația cu Doctorul ---
        // Știrea aparține unui doctor care a realizat intervenția
        [Display(Name = "Medic Responsabil")]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public virtual Doctor? Doctor { get; set; }
    }
}