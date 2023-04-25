using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapsuleIdentity.Data;
using CapsuleIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CapsuleIdentity.Authorizations;

namespace CapsuleIdentity.Controllers
{
    public class VetementsController : Controller
    {
        private ApplicationDbContext Context { get; }
        private IAuthorizationService AuthorizationService { get; }
        private UserManager<IdentityUser> UserManager { get; }

        public VetementsController(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
        {
            Context = context;
            AuthorizationService = authorizationService;
            UserManager = userManager;
        }


        // GET: Vetements
        public async Task<IActionResult> Index(string vetementGenre, string searchString)
        {
            if (Context.Vetement == null)
                return NotFound();
            IQueryable<string> genreQuery = from v in Context.Vetement
                                            orderby v.Genre
                                            select v.Genre;
            var vetements = from v in Context.Vetement select v;

            if (!string.IsNullOrEmpty(searchString))
            {
                vetements = vetements.Where(s => s.Nom!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(vetementGenre))
            {
                vetements = vetements.Where(x => x.Genre == vetementGenre);
            }

            var vetementGenreVM = new VetementGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Vetements = await vetements.ToListAsync()
            };

            

            var isAuthorized = User.
            IsInRole(AuthorizationConstants.VetementAdministratorsRole);
            var currentUserId = UserManager.GetUserId(User);
            if (!isAuthorized)
                vetements = vetements.Where(v => v.ProprietaireId == currentUserId);
            return View(vetementGenreVM);
        }

        // GET: Vetements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || Context.Vetement == null)
            {
                return NotFound();
            }
            var v = await Context.Vetement.FirstOrDefaultAsync(
            m => m.VetementId == id);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
            User, v, VetementOperations.Read);
            if (!isAuthorized.Succeeded)
                return Forbid();
            return View(v);
        }

        // GET: Vetements/Create
        public IActionResult Create()
        {
            if (Context.Vetement == null)
                return NotFound();
            IQueryable<string> genreGenreQuery = from g in Context.GenreVetements
                                            orderby g.NomGenre
                                            select g.NomGenre;
            var genres = from g in Context.GenreVetements select g;

            VetementCreation model = new VetementCreation()
            {
                Genres = new SelectList(genreGenreQuery.Distinct().ToList())
            };
            

            return View(model);

        }

        // POST: Vetements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VetementId,Nom,Genre,Couleur,Description,DateObtention,VetementGenres,Rating")] VetementCreation vc)
        {
            Vetement v = new Vetement();
            if (ModelState.IsValid)
            {
                var currentUserId = UserManager.GetUserId(User);
                vc.ProprietaireId = currentUserId;

                v.ProprietaireId = vc.ProprietaireId;
                v.Nom = vc.Nom;
                v.Couleur = vc.Couleur;
                v.VetementId = vc.VetementId;
                v.Genre = vc.VetementGenres;
                v.Description = vc.Description;
                v.DateObtention = vc.DateObtention;
                v.Rating = vc.Rating;

                Context.Add(v);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(v);
        }

        // GET: Vetements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || Context.Vetement == null)
            {
                return NotFound();
            }

            var vetement = await Context.Vetement.FindAsync(id);
            if (vetement == null)
            {
                return NotFound();
            }
            return View(vetement);
        }

        // POST: Vetements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VetementId,Nom,Genre,Couleur,Description,DateObtention,Rating")] Vetement v)
        {
            if (id != v.VetementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentUserId = UserManager.GetUserId(User);
                    v.ProprietaireId = currentUserId;
                    Context.Update(v);
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VetementExists(v.VetementId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(v);
        }

        // GET: Vetements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || Context.Vetement == null)
            {
                return NotFound();
            }

            var vetement = await Context.Vetement
                .FirstOrDefaultAsync(m => m.VetementId == id);
            if (vetement == null)
            {
                return NotFound();
            }

            return View(vetement);
        }

        // POST: Vetements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (Context.Vetement == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vetement'  is null.");
            }
            var vetement = await Context.Vetement.FindAsync(id);
            if (vetement != null)
            {
                Context.Vetement.Remove(vetement);
            }
            
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VetementExists(int id)
        {
          return (Context.Vetement?.Any(e => e.VetementId == id)).GetValueOrDefault();
        }
    }
}
