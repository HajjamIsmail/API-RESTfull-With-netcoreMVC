using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApp.Models
{
    public class Equipe
    {
        //public Equipe()
        //{
        //    Joueurs = new HashSet<Joueur>();
        //}

        public int Id { get; set; }
        [Display(Name = "Nom Equipe")]
        public string NomE { get; set; }

        //public virtual ICollection<Joueur> Joueurs { get; set; }
    }
}
