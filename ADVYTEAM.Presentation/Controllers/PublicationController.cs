using ADVYTEAM.Data;
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
        // GET: Publication
        public ActionResult Index()
        {
            UserVM userc = Session["userConnected"] as UserVM;
            Session["emp"] = userc.image;
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:9080");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responce = Client.GetAsync("/PIDEV-web/PIDEV/gestionEmploye/publication").Result;
       //     HttpResponseMessage responce2, responce3;
            if (responce.IsSuccessStatusCode)
            {
                IEnumerable<PublicationVM> lstPub = responce.Content.ReadAsAsync<IEnumerable<PublicationVM>>().Result;

                //foreach (var pub in lstPub)
                //{
                //    responce2= Client.GetAsync("/PIDEV-web/PIDEV/gestionEmploye/publication/"+pub.id).Result;
                //    pub.utilisateur= responce2.Content.ReadAsAsync<utilisateur>().Result;
                //    responce3 = Client.GetAsync("/PIDEV-web/PIDEV/gestionEmploye/publication/commentaire/"+ pub.id).Result;
                //    pub.commentaires = responce3.Content.ReadAsAsync<IList<commentaire>>().Result;
                             
                //}

                


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
            return View();
        }

        // POST: Publication/Edit/5
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
