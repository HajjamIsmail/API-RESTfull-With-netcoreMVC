using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApp.Models
{
    public class Joueur
    {
        public int Id { get; set; }
        [Display(Name = "Nom Joueur")]
        public string NomJ { get; set; }
        [Display(Name = "Sexe Joueur")]
        public string SexeJ { get; set; }
        [Display(Name = "Age Joueur")]
        public int? AgeJ { get; set; }
        [Display(Name = "Id Equipe")]
        public int? IdE { get; set; }
        [Display(Name = "Nom Equipe")]
        public string NomE { get;set; }

    }
}
