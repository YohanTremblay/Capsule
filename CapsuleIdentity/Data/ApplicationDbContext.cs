using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CapsuleIdentity.Models;

namespace CapsuleIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<CapsuleIdentity.Models.Vetement>? Vetement { get; set; }
        public DbSet<CapsuleIdentity.Models.GenreVetement>? GenreVetements { get; set; }
    }
}