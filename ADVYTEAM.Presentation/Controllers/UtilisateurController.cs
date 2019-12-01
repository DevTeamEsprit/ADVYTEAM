using ADVYTEAM.Data;
using ADVYTEAM.Presentation.Models;
using ADVYTEAM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            utilisateur user = serviceUtilisateur.Get(u => u.id == (int)userc.id);

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responce = Client.GetAsync("/PIDEV-web/PIDEV/gestionEmploye/publication/user/"+userc.id).Result;

            if (responce.IsSuccessStatusCode)
            {
                IEnumerable<PublicationVM> lstPub = responce.Content.ReadAsAsync<IEnumerable<PublicationVM>>().Result;
                ViewBag.publications = lstPub;
            }


            user.contrat = contratService.Get(contrat=> contrat.reference==(int)user.contrat_reference);
         
            ViewBag.user = user;

            return View();
        }
    }
}