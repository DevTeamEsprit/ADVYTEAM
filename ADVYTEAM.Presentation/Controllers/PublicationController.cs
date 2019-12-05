using ADVYTEAM.Data;
using ADVYTEAM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ADVYTEAM.Presentation.Models
{
    public class PublicationController : Controller
    {
        IPublicationService publicationService;
       

        public PublicationController()
        {
            publicationService = new PublicationService();
             
        }

            // GET: Publication
            public ActionResult Index()
        {
            UserVM userc = Session["userConnected"] as UserVM;
            if (userc == null)
                return RedirectToAction("Create", "Login");

            Session["emp"] = userc.image;
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responce = Client.GetAsync("/PIDEV-web/PIDEV/gestionEmploye/publication").Result;
       //     HttpResponseMessage responce2, responce3;
            if (responce.IsSuccessStatusCode)
            {
                IEnumerable<PublicationVM> lstPub = responce.Content.ReadAsAsync<IEnumerable<PublicationVM>>().Result;
                ViewBag.result = lstPub;
            }
            else
            {
                ViewBag.result = "error";
            }          

            return View();
        }

        // GET: Publication/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Publication/Create
        public ActionResult Create()
        {
            UserVM userc = Session["userConnected"] as UserVM;
            if (userc == null)
                return RedirectToAction("Create", "Login");
            return View();
        }

        // POST: Publication/Create
        [HttpPost]
        public ActionResult Create(PublicationVM publicationvm)
        {
            UserVM userc = Session["userConnected"] as UserVM;
            Session["emp"] = userc.image;

            publication p = new publication()
            {
                description=publicationvm.description,
                id_user = userc.id

            };

            HttpClient client = new HttpClient();

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:9080/PIDEV-web/PIDEV/gestionEmploye/publication");
            //requestMessage.Headers.Add("Authorization", "key=AAAAG...:APA91bH7U...");
            string json = new JavaScriptSerializer().Serialize(new
            {
                description = p.description,
                user = new { id = userc.id }
            });

            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
           
            HttpResponseMessage response = client.SendAsync(requestMessage).GetAwaiter().GetResult();
            return RedirectToAction("Index");

        }

        // GET: Publication/Edit/5
        public ActionResult Edit(int id)
        {
            UserVM userc = Session["userConnected"] as UserVM;
            if (userc == null)
                return RedirectToAction("Create", "Login");


            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responce = Client.GetAsync("/PIDEV-web/PIDEV/gestionEmploye/publication/"+id).Result;

            PublicationVM Pub = responce.Content.ReadAsAsync<PublicationVM>().Result;

            ViewBag.userimg = userc.image;


            return View(Pub);
        }

        // POST: Publication/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PublicationVM publicationVM)
        {

            publication pub = publicationService.GetById(id);
            pub.description = publicationVM.description;

            publicationService.Update(pub);
            publicationService.Commit();


                return RedirectToAction("Index");
            
        }

        // GET: Publication/Delete/5
        public ActionResult Delete(int id)
        {

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responce = Client.DeleteAsync("/PIDEV-web/PIDEV/gestionEmploye/publication/"+id).Result;
            return RedirectToAction("Index");

        }

        // POST: Publication/Delete/5
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
