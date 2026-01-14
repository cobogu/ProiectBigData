using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectBigData.Models
{
    public class Programare
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele pacientului este obligatoriu")]
        [Display(Name = "Nume Pacient")]
        public string NumePacient { get; set; }

        [Required(ErrorMessage = "Data programării este obligatorie")]
        [Display(Name = "Data și Ora")]
        public DateTime Data { get; set; }

        // --- Relația cu Doctorul ---
        // Alegem doctorul, iar el ne "spune" automat spitalul și adresa
        [Required(ErrorMessage = "Trebuie să alegeți un doctor")]
        [ForeignKey("Doctor")]
        public int DoctorId  { get; set; }

        public virtual Doctor? Doctor { get; set; }
    }
}