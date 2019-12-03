using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADVYTEAM.Data;
using ADVYTEAM.Presentation.Models;
using ADVYTEAM.Services;

namespace ADVYTEAM.Presentation.Controllers
{
    public class FormationController : Controller
    {
        IFormationService fs = new FormationService();
        
        //** affichage liste
        public ActionResult Index()
        {
            var list = fs.GetMany();

            var formations = new List<FormationVM>();
            foreach (formation f in list)
            {
                formations.Add(new FormationVM()
                {
                    id=f.id,
                    decription = f.decription,
                    nbrjour = f.nbrjour,
                    dateformation = f.dateformation

                });
            }
            return View(formations);
        }

        //**ajout formation
        public ActionResult createformation ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult createformation(FormationVM formationVm)
        {
            if (formationVm != null)
            {
                formation f = new formation
                {
                    dateformation = formationVm.dateformation,
                    decription = formationVm.decription,
                    nbrjour = formationVm.nbrjour
                };
                fs.Add(f);
                fs.Commit();
            }
            return View();
        }

        //**Edit
        public ActionResult updateformation( int id)
        {
            formation f = fs.GetById(id);
            FormationVM fvm = new FormationVM {
            id=f.id,
            decription= f.decription,
            nbrjour=f.nbrjour,
            dateformation = f.dateformation
            
              };
            return View(fvm);
        }
        [HttpPost]
        public ActionResult updateformation(FormationVM formationVm)
        {
            if (formationVm != null)
            {
                formation f = new formation
                {   id=formationVm.id,
                    dateformation = formationVm.dateformation,
                    decription = formationVm.decription,
                    nbrjour = formationVm.nbrjour
                };
                fs.Update(f);
                fs.Commit();
            }
            return View();
        }
    
       //**Delete
       public void  Delete (int id)
        {
            formation f = fs.GetById(id);
            fs.Delete(f);
            fs.Commit();
        }

        //**detail formation
        public ActionResult Details ( int id )
        {
            formation f = fs.GetById(id);
            FormationVM fvm = new FormationVM
            {
                id = f.id,
                decription = f.decription,
                nbrjour = f.nbrjour,
                dateformation = f.dateformation

            };
            return View(fvm);
        } 
    }
}