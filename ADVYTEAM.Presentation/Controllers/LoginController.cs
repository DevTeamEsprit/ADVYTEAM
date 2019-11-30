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
    public class LoginController : Controller
    {
        HttpClient Client;
        HttpResponseMessage responce;
        utilisateur user;
        IUtilisateurService serviceUtilisateur;
        IList<UserVM> lstcontact;

        public LoginController()
        {
            serviceUtilisateur = new UtilisateurService();
            lstcontact = new List<UserVM>();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View(user);
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(LoginVM loginVm)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            responce = Client.GetAsync("/PIDEV-web/PIDEV/gestionEmploye/login?login="+loginVm.login+"&pass="+loginVm.password).Result;

            user = responce.Content.ReadAsAsync<utilisateur>().Result;

            if (user != null)
            {
                UserVM u = new UserVM
                {
                    prenom = user.prenom,
                    nom = user.nom,
                    image = user.image,
                    id = user.id
                };
                Session["userConnected"] = u;

                foreach (utilisateur user in serviceUtilisateur.GetMany())
                {
                    lstcontact.Add(new UserVM()
                    {
                        id = user.id,
                        nom = user.nom,
                        prenom = user.prenom,
                        image = user.image
                    });
                }

                Session["lstContact"] = lstcontact;

                return Redirect("~/Publication/Index");
                
            }
            return View();
            
        }

        public ActionResult LogOff()
        {
            Session.Contents.RemoveAll();

            return RedirectToAction("Create");
        }

        public ActionResult Password()
        {

            return RedirectToAction("Create");
        }
    }
}
