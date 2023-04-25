using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CapsuleIdentity.Models
{
    public class VetementCreation
    {
        public int VetementId { get; set; }
        public string Nom { get; set; }
        SelectList? Genres { get; set; }
        public string VetementGenres { get; set; }
        public string Description { get; set; }
        public DateTime DateObtention { get; set; }


        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), Required, StringLength(30)]
        public string? Couleur { get; set; }

        public string? ProprietaireId { get; set; }
    }
}
