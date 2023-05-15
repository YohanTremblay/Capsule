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

            var isAuthorized = User.
            IsInRole(AuthorizationConstants.VetementAdministratorsRole);
            var currentUserId = UserManager.GetUserId(User);
            if (!isAuthorized)
                vetements = vetements.Where(v => v.ProprietaireId == currentUserId);
            var vetementGenreVM = new VetementGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Vetements = await vetements.ToListAsync()
            };
            return View(vetementGenreVM);
        }
        // GET: VetementsRandom
        public async Task<IActionResult> Random(string vetementGenre, string searchString)
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

            var isAuthorized = User.
            IsInRole(AuthorizationConstants.VetementAdministratorsRole);
            var currentUserId = UserManager.GetUserId(User);
            if (!isAuthorized)
            {
                vetements = vetements.Where(v => v.ProprietaireId == currentUserId);
            }
            var chapeaux = vetements.Where(v => v.Genre == "Chapeau");
            var lstChap = new List<Vetement>();
            if (chapeaux.Any())
            {
                foreach (var item in chapeaux)
                {
                    for (int i = 0; i < item.Rating; i++)
                    {
                        lstChap.Add(item);
                    }

                }
            }
            int countChapeaux = lstChap.Count();

            var chaussures = vetements.Where(v => v.Genre == "Chaussure");
            var lstChau = new List<Vetement>();
            if(chaussures.Any())
            {
                foreach (var item in chaussures)
                {
                    for (int i = 0; i < item.Rating; i++)
                    {
                        lstChau.Add(item);
                    }

                }
            }
            int countChaussures = lstChau.Count();

            var bas = vetements.Where(v => v.Genre == "Bas");
            var lstBas = new List<Vetement>();
            if(bas.Any())
            {
                foreach (var item in bas)
                {
                    for (int i = 0; i < item.Rating; i++)
                    {
                        lstBas.Add(item);
                    }

                }
            }
            int countBas = lstBas.Count();

            var hauts = vetements.Where(v => v.Genre == "Haut");
            var lstHauts = new List<Vetement>();
            if(hauts.Any())
            {
                foreach (var item in hauts)
                {
                    for (int i = 0; i < item.Rating; i++)
                    {
                        lstHauts.Add(item);
                    }

                }
            }
            int countHauts = lstHauts.Count();

            var deuxPieces = vetements.Where(v => v.Genre == "Deux pièces");
            var lst2Pieces = new List<Vetement>();
            if(deuxPieces.Any())
            {
                foreach (var item in deuxPieces)
                {
                    for (int i = 0; i < item.Rating; i++)
                    {
                        lst2Pieces.Add(item);
                    }

                }
            }
            
            int countDeuxPieces = lst2Pieces.Count();

            Random rnd = new Random();
            var vetementsLst = new List<Vetement>();

            
          
            if (countDeuxPieces != 0 && countHauts != 0 )
            {
                int nbRND = rnd.Next(1);
                if(nbRND == 0)
                {
                    if (countChaussures != 0)
                    {
                        int chaussuresRND = rnd.Next(countChaussures);
                        var chaussure = lstChau.ElementAt(chaussuresRND);
                        vetementsLst.Add(chaussure);
                    }

                    if (countChapeaux != 0)
                    {
                        int chapeauxRND = rnd.Next(countChapeaux);
                        var chapeau = lstChap.ElementAt(chapeauxRND);
                        vetementsLst.Add(chapeau);
                    }

                    if (countBas != 0)
                    {
                        int basRND = rnd.Next(countBas);
                        var ba = lstBas.ElementAt(basRND);
                        vetementsLst.Add(ba);
                    }
                    int hautRND = rnd.Next(countHauts);
                    var haut = lstHauts.ElementAt(hautRND);
                    vetementsLst.Add(haut);
                }
                else if(nbRND == 1)
                {
                    if (countChaussures != 0)
                    {
                        int chaussuresRND = rnd.Next(countChaussures);
                        var chaussure = lstChau.ElementAt(chaussuresRND);
                        vetementsLst.Add(chaussure);
                    }

                    if (countChapeaux != 0)
                    {
                        int chapeauxRND = rnd.Next(countChapeaux);
                        var chapeau = lstChap.ElementAt(chapeauxRND);
                        vetementsLst.Add(chapeau);
                    }
                    int deuxPiecesRND = rnd.Next(countDeuxPieces);
                    var deuxPiece = lst2Pieces.ElementAt(deuxPiecesRND);
                    vetementsLst.Add(deuxPiece);
                }
            }
            else if(countHauts == 0 && countDeuxPieces != 0)
            {
                if (countChaussures != 0)
                {
                    int chaussuresRND = rnd.Next(countChaussures);
                    var chaussure = lstChau.ElementAt(chaussuresRND);
                    vetementsLst.Add(chaussure);
                }

                if (countChapeaux != 0)
                {
                    int chapeauxRND = rnd.Next(countChapeaux);
                    var chapeau = lstChap.ElementAt(chapeauxRND);
                    vetementsLst.Add(chapeau);
                }
                int deuxPiecesRND = rnd.Next(countDeuxPieces);
                var deuxPiece = lst2Pieces.ElementAt(deuxPiecesRND);
                vetementsLst.Add(deuxPiece);
            }
            else if(countHauts != 0 && countDeuxPieces == 0)
            {
                if (countChaussures != 0)
                {
                    int chaussuresRND = rnd.Next(countChaussures);
                    var chaussure = lstChau.ElementAt(chaussuresRND);
                    vetementsLst.Add(chaussure);
                }

                if (countChapeaux != 0)
                {
                    int chapeauxRND = rnd.Next(countChapeaux);
                    var chapeau = lstChap.ElementAt(chapeauxRND);
                    vetementsLst.Add(chapeau);
                }

                if (countBas != 0)
                {
                    int basRND = rnd.Next(countBas);
                    var ba = lstBas.ElementAt(basRND);
                    vetementsLst.Add(ba);
                }
                int hautRND = rnd.Next(countHauts);
                var haut = lstHauts.ElementAt(hautRND);
                vetementsLst.Add(haut);
            }

            var VetementRandom = new VetementRandom
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Vetements = vetementsLst
            };
            return View(VetementRandom);
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
        public async Task<IActionResult> Create([Bind("VetementId,Nom,Genre,Couleur,Description,DateObtention,VetementGenres,Rating,Image")] VetementCreation vc)
        {
            Vetement v = new Vetement();
            if (ModelState.IsValid)
            {
                var currentUserId = UserManager.GetUserId(User);
                vc.ProprietaireId = currentUserId;

                v.ProprietaireId = vc.ProprietaireId;
                v.Nom = vc.Nom;
                v.Couleur = vc.Couleur;
                v.Genre = vc.VetementGenres;
                v.Description = vc.Description;
                v.DateObtention = vc.DateObtention;
                v.Rating = vc.Rating;
                v.Image = "";

                Context.Add(v);
                await Context.SaveChangesAsync();

                var vetements = from vi in Context.Vetement select vi;
                int max = Context.Vetement.Max((x) => x.VetementId);
                Vetement vetement = Context.Vetement.Where((x) => x.VetementId == max).FirstOrDefault();

                var pathDossier = Path.Combine("wwwroot/images/", vetement.VetementId.ToString());
                Directory.CreateDirectory(pathDossier);

                var imageName = Path.GetFileName(vc.Image.FileName);
                var imageExtension = Path.GetExtension(vc.Image.FileName);
                var imageFileName = Path.GetFileNameWithoutExtension(imageName);

                var path = Path.Combine(pathDossier, imageFileName + imageExtension);
                var ecriture = System.IO.File.Create(path);

                await vc.Image.CopyToAsync(ecriture);

                vetement.Image = "~/images/"+ vetement.VetementId + "/" + imageFileName + imageExtension;

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
        public async Task<IActionResult> Edit(int id, [Bind("VetementId,Nom,Genre,Couleur,Description,DateObtention,Rating,Image")] Vetement v)
        {
            if (id != v.VetementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //var vetements = from vi in Context.Vetement select vi;
                    //vetements = vetements.Where(x => x.VetementId == v.VetementId);

                    var currentUserId = UserManager.GetUserId(User);
                    v.ProprietaireId = currentUserId;

                    //var premierVetement = vetements.FirstOrDefault(); // Obtenir le premier élément correspondant à la condition
                    //if (premierVetement != null)
                    //{
                    //    v.Image = premierVetement.Image; // Affecter la propriété Image à v.Image
                    //}
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
                var pathDossier = Path.Combine("wwwroot/images/", vetement.VetementId.ToString());
                Directory.Delete(pathDossier, true);
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
