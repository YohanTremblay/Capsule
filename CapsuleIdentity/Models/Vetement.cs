using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CapsuleIdentity.Models
{
    public class Vetement
    {
        public int VetementId { get; set; }
        public string Nom { get; set; }
        public string? Genre { get; set; }
        public string Description { get; set; }
        public DateTime DateObtention { get; set; }
        public int Rating { get; set; }
        public string Image { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), Required, StringLength(30)]
        public string? Couleur { get; set; }

        public string? ProprietaireId { get; set; }
    }
}
