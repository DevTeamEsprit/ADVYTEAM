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
    public class ChatController : Controller
    {
        IUtilisateurService serviceUtilisateur;
        IMessageService messageService;
        UserVM userVm, userc , userch;
        utilisateur user;
        public ChatController()
        {
            serviceUtilisateur = new UtilisateurService();
            messageService = new MessageService();
       
        }
        public string envoiMessage(string message)
        {
            userc = Session["userConnected"] as UserVM;
            userch = Session["userchat"] as UserVM;

            message m = new message()
            {
                content = message,
                date = DateTime.Now,
                id_receiver = userch.id,
                id_sender = userc.id
            };

            this.messageService.Add(m);
            this.messageService.Commit();


            return discussion();
        }
        public string discussion()
        {
            userc = Session["userConnected"] as UserVM;
            userch = Session["userchat"] as UserVM;
              IEnumerable<message> lstM = this.messageService.GetMessagesBySenderReciver(userc.id, userch.id);
            //IEnumerable<message> lstM = this.messageService.GetMany(
            //                        message => (message.id_receiver == userc.id &&
            //                                   message.id_sender == userch.id) ||
            //                                   (message.id_receiver == userch.id &&
            //                                   message.id_sender == userc.id)
            //                        );
            var chaineM= "";
            foreach(message msg in lstM)
            {
                if (msg.id_sender == userc.id)
                {
                    chaineM += "<div class='direct-chat-msg right'>" +
                        "<div class='direct-chat-info clearfix'>" +
                            "<span class='direct-chat-timestamp float-left'>" + msg.date + "</span>" +
                        "</div>" +
                    "<img class='direct-chat-img' src='data:image/png;base64," + userc.image + "' alt='message user image'>" +
                        "<div class='direct-chat-text'>" +
                                msg.content+
                       "</div>" +
                    "</div>";
                }
                else
                {
                    chaineM += "<div class='direct-chat-msg'>" +
                                "<div class='direct-chat-info clearfix'>" +
                                "<span class='direct-chat-timestamp float-right'>" + msg.date + "</span>" +
                                "</div>" +
                                "<img class='direct-chat-img' src='data:image/png;base64," + userch.image + "' alt='message user image'>" +
                                "<div class='direct-chat-text'>" +
                                    msg.content +
                                "</div>" +
                                "</div>";
                }

            }

            return chaineM;
        }

        // GET: Chat
        public ActionResult Index(string idreciver )
        {
            userc = Session["userConnected"] as UserVM;
            user= this.serviceUtilisateur.GetById(System.Convert.ToInt32(idreciver));
            Session["userchat"] = userVm;
            userVm = new UserVM()
            {
                id = user.id,
                nom = user.nom,
                prenom=user.prenom,
                image=user.image
            };
            Session["userchat"] = userVm;
            ViewBag.userc = userc;
            return View(userVm);
        }

        // GET: Chat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Chat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chat/Create
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

        // GET: Chat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Chat/Edit/5
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

        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Chat/Delete/5
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

        public ActionResult ChatRoom()
        {
            return View();
        }
    }
}
