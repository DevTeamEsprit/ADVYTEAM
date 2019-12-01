using ADVYTEAM.Data;
using ADVYTEAM.Presentation.Models;
using ADVYTEAM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADVYTEAM.Presentation.Controllers
{
    public class UtilisateurController : Controller
    {
        IUtilisateurService serviceUtilisateur;
        IList<UserVM> lstcontact;
        IContratService contratService;
        IPublicationService publicationService;

        public UtilisateurController()
        {
            serviceUtilisateur = new UtilisateurService();
            contratService = new ContratService();
            lstcontact = new List<UserVM>();
            publicationService = new PublicationService();
        }

        public ActionResult ListContact()
        {

            foreach (utilisateur u in serviceUtilisateur.GetMany())
            {
                lstcontact.Add(new UserVM()
                {
                    id = u.id,
                    nom = u.nom,
                    prenom = u.prenom,
                    image = u.image
                });
            }

            return View(lstcontact);
        }

        public ActionResult Profile()
        {
            UserVM userc = Session["userConnected"] as UserVM;
            utilisateur user = serviceUtilisateur.Get(u=>u.id==(int)userc.id);
      
            user.contrat = contratService.Get(contrat=> contrat.reference==(int)user.contrat_reference);
        //    user.publications = publicationService.GetMany(publication=>publication.id_user==user.id).ToList();
            ViewBag.user = user;

            return View(user);
        }
    }
}