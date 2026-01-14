namespace ProiectBigData.Models
{
    public class Spital
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Adresa { get; set; }

        public int NrPersonal { get; set; }

        public int NrPaturiTotal { get; set; }
        public int NrPaturiOcupate { get; set; }
    }
}
