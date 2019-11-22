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
      //  IDataBaseFactory factory;
      //  IUnitOfWork uow;
       
       
     //   IService<reclamation> serReclamation;
     //   IService<utilisateur> serUser;
             IEnumerable<reclamation> lstRecl;
             IReclamationService serviceReclamation;
             IUtilisateurService serviceUtilisateur;

        public ReclamationController()
        {
            serviceReclamation = new ReclamationService();
            serviceUtilisateur = new UtilisateurService();
            //   factory = new DataBaseFactory();
            //   uow = new UnitOfWork(factory);
            //    serReclamation = new Service<reclamation>(uow);
            //    serUser = new Service<utilisateur>(uow);
        }
        // GET: Reclamation
        public ActionResult Index()
        {
            lstRecl = serviceReclamation.GetMany();
            foreach (var rec in lstRecl)
            {
                rec.Utilisateur = serviceUtilisateur.GetById((int)rec.UserId);
            }
                /*    lstRecl = serReclamation.GetMany();
                    serReclamation.Dispose();
                    foreach (var rec in lstRecl)
                    {
                        rec.Utilisateur = serUser.GetById((int)rec.UserId);
                    }*/
                return View(lstRecl);
        }

        // GET: Reclamation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reclamation/Create
        public ActionResult Create()
        {
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
                    Etat = false,
                    UserId = userc.id
                    
                };
                utilisateur user = serviceUtilisateur.GetById((int)userc.id);
                user.Reclamations.Add(r);
                serviceUtilisateur.Commit();
 

                return RedirectToAction("Index");
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
