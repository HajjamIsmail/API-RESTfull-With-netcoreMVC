namespace WebApp.Models
{
    public class Joueur
    {
        public int Id { get; set; }
        public string NomJ { get; set; }
        public string SexeJ { get; set; }
        public int? AgeJ { get; set; }
        public int? IdE { get; set; }

        public virtual Equipe IdENavigation { get; set; }
    }
}
