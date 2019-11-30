using ADVYTEAM.Data;
using ADVYTEAM.Domain.Entities;
using ADVYTEAM.Presentation.Models;
using ADVYTEAM.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ADVYTEAM.Presentation.Controllers
{
    public class ReclamationController : Controller
    {
 
             IEnumerable<reclamation> lstRecl;
        IEnumerable<reclamation> lstReclt;
        IEnumerable<reclamation> lstReclc;
        IEnumerable<reclamation> lstRecln;
        IReclamationService serviceReclamation;
             IUtilisateurService serviceUtilisateur;

        public ReclamationController()
        {
            serviceReclamation = new ReclamationService();
            serviceUtilisateur = new UtilisateurService();
 
        }

        public ActionResult updateRecl(int id)
        {
            reclamation r = serviceReclamation.Get(f => f.Id == id);
            utilisateur us = serviceUtilisateur.Get(user => user.id == r.UserId);

            if (r.Etat == 0)
                r.Etat = 1;
            else if (r.Etat == 1)
            {
                r.Etat = 2;
                r.DateTraitement = DateTime.Now;
            }

            foreach (reclamation rec in us.Reclamations)
            {
             if(rec.Id==id)
                {
                    if (rec.Etat == 0)
                        rec.Etat = 1;
                    else if (rec.Etat == 1) { 
                        rec.Etat = 2;
                    rec.DateTraitement=DateTime.Now;
                }
                }
            } 
            serviceUtilisateur.Commit();
            return RedirectToAction("Index");
        }


        public ActionResult MesReclamation()
        {
            UserVM userc = Session["userConnected"] as UserVM;

            //   lstRecl = serviceReclamation.GetMany(reclamation=>reclamation.UserId== userc.id);
            lstRecl = serviceReclamation.GetMesReclmations(userc.id);

            foreach (var rec in lstRecl)
            {
                rec.Utilisateur = serviceUtilisateur.GetById((int)rec.UserId);
            }
            ViewBag.listReclamtion = lstRecl;

            return View();
        }


            // GET: Reclamation
        public ActionResult Index()
        {
            lstRecl = null;
            UserVM userc = Session["userConnected"] as UserVM;
            if (userc == null)
                return RedirectToAction("Create", "Login");

            lstRecln = serviceReclamation.GetReclmationsAttent();
            lstReclc = serviceReclamation.GetReclmationsEnCour();
            lstReclt = serviceReclamation.GetReclmationsTraite();
            // lstRecl = serviceReclamation.GetMany();
            foreach (var rec in lstRecln)
            {
                rec.Utilisateur = serviceUtilisateur.GetById((int)rec.UserId);
            }
            foreach (var rec in lstReclc)
            {
                rec.Utilisateur = serviceUtilisateur.GetById((int)rec.UserId);
            }
            foreach (var rec in lstReclt)
            {
                rec.Utilisateur = serviceUtilisateur.GetById((int)rec.UserId);
            }
            ViewBag.reclamationAttnte = lstRecln;
            ViewBag.reclamationCours = lstReclc;
            ViewBag.reclamationTraite= lstReclt;


            return View();
        }

        // GET: Reclamation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reclamation/Create
        public ActionResult Create()
        {
            UserVM userc = Session["userConnected"] as UserVM;
            if (userc == null)
                return RedirectToAction("Create", "Login");
            else

            return View();
        }

        // POST: Reclamation/Create
        [HttpPost]
        public ActionResult Create(ReclamationVM reclamationVM)
        {
            UserVM userc = Session["userConnected"] as UserVM;

            if (ModelState.IsValid)
            {
                reclamation r = new reclamation()
                {
                    Objet = reclamationVM.Objet,
                    Description = reclamationVM.Description,
                    DateReclamation = DateTime.Now,
                    DateTraitement = null,
                    Etat = 0,
                    UserId = userc.id
                    
                };

              //  serviceReclamation.Add(r);
              //  serviceReclamation.Commit();
                utilisateur user = serviceUtilisateur.GetById((int)userc.id);
                user.Reclamations.Add(r);
                serviceUtilisateur.Commit();
 

                return RedirectToAction("MesReclamation");
            }
            return View();
        }

        // GET: Reclamation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reclamation/Edit/5
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

        // GET: Reclamation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reclamation/Delete/5
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
