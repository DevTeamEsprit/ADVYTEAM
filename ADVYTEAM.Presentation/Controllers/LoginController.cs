using ADVYTEAM.Data;
using ADVYTEAM.Presentation.Models;
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
                Session["userid"] = user.id;
                Session["usernom"] = user.nom +" "+user.prenom;
                Session["userimg"] = user.image;
                return Redirect("~/Publication/Index");
                // return RedirectToAction("Index");
            }
            return View();
            
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
