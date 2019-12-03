using ADVYTEAM.Data;
using ADVYTEAM.Presentation.Models;
using ADVYTEAM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

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
            if (responce.IsSuccessStatusCode)
            {

                user = responce.Content.ReadAsAsync<utilisateur>().Result;
 
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

        public ActionResult OubliePassword(LoginVM loginVm)
        {
 
                return View();
        }

        public ActionResult traiteCode(LoginVM loginVm)
        {
            utilisateur user = serviceUtilisateur.Get(u => u.email.Equals(loginVm.login));
            if (user != null)
            {
                Random aleatoire = new Random();
                int code = aleatoire.Next(1000, 9999);

 
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("porjet2019@gmail.com");
                mail.To.Add(user.email);
                mail.Subject = "Oublié mot de passe";
                mail.Body = "Bonjour M/Mme " + user.nom + " " + user.prenom + "  Votre Code est : " + code;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("porjet2019@gmail.com", "duwhjaxubkjxebih");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

                Session["mail"] = user.email;
                Session["code"] = code;
                Session["user"] = user;

                return RedirectToAction("newPassword");
            }
            else
                return View();
        }

        public ActionResult newPassword()
        {
            utilisateur u = Session["user"] as utilisateur;

         //   string email = Session["mail"].ToString();
            string code = Session["code"].ToString();

            oubliePass oublie = new oubliePass();
            oublie.email = u.email;

            return View(oublie);
        }

        public ActionResult changerPassword(oubliePass oublie)
        {
            utilisateur u = Session["user"] as utilisateur;
            int code = Int32.Parse(Session["code"].ToString());
            if (code == oublie.Code && oublie.password.Equals(oublie.confpassword)) {

                u.password = MD5Hash(oublie.password);

                serviceUtilisateur.Commit();

                return RedirectToAction("Create");
               
            } 
            return RedirectToAction("OubliePassword");
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
