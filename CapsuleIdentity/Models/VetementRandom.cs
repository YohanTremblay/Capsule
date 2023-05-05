using Microsoft.AspNetCore.Mvc.Rendering;

namespace CapsuleIdentity.Models
{
    public class VetementRandom
    {
        public List<Vetement>? Vetements { get; set; }
        public SelectList? Genres { get; set; }
        public List<GenreVetement>? VetementGenre { get; set; }
        public string? SearchString { get; set; }
    }
}
