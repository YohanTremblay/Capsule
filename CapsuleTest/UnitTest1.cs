using CapsuleIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CapsuleTest
{
    [TestClass]
    public class ModificationBD
    {
        Mock<ApplicationDbContext> mockContext;

        [TestInitialize]
        public void Init()
        {
            mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
        }

        [TestMethod]
        public void InsertionBD()
        {
            DateTime date = new DateTime();
            Vetement vetement = new Vetement
            {
                Couleur = "Rouge",
                DateObtention = date,
                Description = "Rouge",
                Genre = "Rouge ?",
                Image = "Lien vers l'image",
                Nom = "Vetement Rouge",
                ProprietaireId = "1",
                Rating = 5,
                VetementId = 1
            };
            
            var vetements = new List<Vetement>
            {

            }.AsQueryable();

            mockContext.Setup(x => x.Vetement).ReturnsDbSet(vetements);

            mockContext.Object.Add(vetement);
            
            mockContext.Verify(x => x.Add(vetement), Times.Once);
            mockContext.Verify(x => x.Add(It.IsAny<Vetement>()), Times.Once);
        }

        [TestMethod]
        public void RetraitBD()
        {
            DateTime date = new DateTime();
            Vetement vetement = new Vetement
            {
                Couleur = "Rouge",
                DateObtention = date,
                Description = "Rouge",
                Genre = "Rouge ?",
                Image = "Lien vers l'image",
                Nom = "Vetement Rouge",
                ProprietaireId = "1",
                Rating = 5,
                VetementId = 1
            };
            
            var vetements = new List<Vetement>
            {
                vetement
            }.AsQueryable();

            mockContext.Setup(x => x.Vetement).ReturnsDbSet(vetements);
            mockContext.Object.Remove(vetement);
            mockContext.Verify(x => x.Remove(vetement), Times.Once);
            mockContext.Verify(x => x.Remove(It.IsAny<Vetement>()), Times.Once);
        }

        [TestMethod]
        public void UpdateBD()
        {
            DateTime date = new DateTime();
            Vetement vetement = new Vetement
            {
                Couleur = "Rouge",
                DateObtention = date,
                Description = "Rouge",
                Genre = "Rouge ?",
                Image = "Lien vers l'image",
                Nom = "Vetement Rouge",
                ProprietaireId = "1",
                Rating = 5,
                VetementId = 1
            };

            var vetements = new List<Vetement>
            {
                vetement
            }.AsQueryable();

            mockContext.Setup(x => x.Vetement).ReturnsDbSet(vetements);

            vetement.Couleur = "Bleu";

            mockContext.Object.Update(vetement);
            mockContext.Verify(x => x.Update(It.IsAny<Vetement>()), Times.Once);
        }
    }

    //[TestClass]
    //public class Index
    //{
    //    Mock<ApplicationDbContext> contextMock;
    //    Mock<IAuthorizationService> authorizationServiceMock;
    //    Mock<UserManager<IdentityUser>> userManagerMock;
    //    VetementsController vetementsController;

    //    [TestInitialize]
    //    public void Init()
    //    {
    //        contextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
    //        authorizationServiceMock = new Mock<IAuthorizationService>();
    //        userManagerMock = new Mock<UserManager<IdentityUser>>();

    //        vetementsController = new VetementsController(contextMock.Object,
    //                                                        authorizationServiceMock.Object,
    //                                                        userManagerMock.Object);
    //    }

    //    [TestMethod]
    //    public void RetourIndexStringOK()
    //    {
    //        DateTime date = new DateTime();
    //        Vetement vetement = new Vetement
    //        {
    //            Couleur = "Rouge",
    //            DateObtention = date,
    //            Description = "Rouge",
    //            Genre = "Rouge ?",
    //            Image = "Lien vers l'image",
    //            Nom = "Vetement Rouge",
    //            ProprietaireId = "1",
    //            Rating = 5,
    //            VetementId = 1
    //        };
    //    }

    //    [TestMethod]
    //    public void RetourIndexStringPasOK()
    //    {
    //        DateTime date = new DateTime();
    //        var vetements = new List<Vetement>
    //        {
    //            new Vetement
    //            {
    //                Couleur = "Rouge",
    //                DateObtention = date,
    //                Description = "Rouge",
    //                Genre = "Rouge ?",
    //                Image = "Lien vers l'image",
    //                Nom = "Vetement Rouge",
    //                ProprietaireId = "1",
    //                Rating = 5,
    //                VetementId = 1
    //            },
    //            new Vetement
    //            {
    //                Couleur = "Bleu",
    //                DateObtention = date,
    //                Description = "Bleu",
    //                Genre = "Bleu ?",
    //                Image = "Lien vers l'image",
    //                Nom = "Vetement Bleu",
    //                ProprietaireId = "1",
    //                Rating = 3,
    //                VetementId = 1
    //            }
    //        }.AsQueryable();

    //        contextMock.Setup(x => x.Vetement).ReturnsDbSet(vetements);
    //        string recherche = "Rouge";
    //        var retour = vetementsController.Index(null, recherche);
    //        var pute = retour.Result;
    //        Assert.AreEqual(retour.Result, true);
    //    }

    //    [TestMethod]
    //    public void RetourIndexGenre()
    //    {
    //        DateTime date = new DateTime();
    //        Vetement vetement = new Vetement
    //        {
    //            Couleur = "Rouge",
    //            DateObtention = date,
    //            Description = "Rouge",
    //            Genre = "Rouge ?",
    //            Image = "Lien vers l'image",
    //            Nom = "Vetement Rouge",
    //            ProprietaireId = "1",
    //            Rating = 5,
    //            VetementId = 1
    //        };
    //    }
    //}
}