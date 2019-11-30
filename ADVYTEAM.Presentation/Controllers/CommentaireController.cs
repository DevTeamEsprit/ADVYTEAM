using ADVYTEAM.Data;
using ADVYTEAM.Presentation.Models;
using ADVYTEAM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ADVYTEAM.Presentation.Controllers
{
    public class CommentaireController : Controller
    {
        IPublicationService publicationService;

        public CommentaireController()
        {
            publicationService = new PublicationService();
        }
        // GET: Commentaire
        public ActionResult Index()
        {
            return View();
        }

        // GET: Commentaire/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Commentaire/Create
        public ActionResult Create(int idpub, string description)
        {
            UserVM userc = Session["userConnected"] as UserVM;
            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:9080/PIDEV-web/PIDEV/gestionEmploye/publication/commentaire/"+idpub);
            //requestMessage.Headers.Add("Authorization", "key=AAAAG...:APA91bH7U...");
            string json = new JavaScriptSerializer().Serialize(new
            {
                description = description,
                user = new { id = userc.id }
            });

            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.SendAsync(requestMessage).GetAwaiter().GetResult();
            return RedirectToAction("Index", "Publication");
        }

        // POST: Commentaire/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Commentaire/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Commentaire/Edit/5
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

        // GET: Commentaire/Delete/5
        public ActionResult Delete(int id)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responce = Client.DeleteAsync("/PIDEV-web/PIDEV/gestionEmploye/publication/commentaire/" + id).Result;
            return RedirectToAction("Index", "Publication");
        }

        // POST: Commentaire/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
