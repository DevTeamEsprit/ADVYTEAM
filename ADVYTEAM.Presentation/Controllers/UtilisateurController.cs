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

        public UtilisateurController()
        {
            serviceUtilisateur= new UtilisateurService();
            lstcontact = new List<UserVM>();
        }

        public ActionResult ListContact()
        {
  
            foreach (utilisateur u in serviceUtilisateur.GetMany())
            {
                lstcontact.Add(new UserVM()
                { 
                    id=u.id,
                    nom=u.nom,
                    prenom=u.prenom,
                    image=u.image
                });
            }

            return View(lstcontact);
        }
    }
}
