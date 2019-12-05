using ADVYTEAM.Domain.Entities;
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
    public class SurveyClientController : Controller
    {
        readonly HttpClient Client = new HttpClient();
        static List<SurveyQuestion> questions = new List<SurveyQuestion>();
        UtilisateurService us = new UtilisateurService();
        SurveyService surveyservice;
        SurveyQuestEmpService surveyquestempservice;
        ISurveyQuestionService surquestservice;

        public SurveyClientController()
        {
            this.Client.BaseAddress = new Uri("http://localhost:9080/PIDEV-web/");
            this.Client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            surveyservice = new SurveyService();
            surquestservice = new SurveyQuestionService();
            surveyquestempservice = new SurveyQuestEmpService();
        }
        // GET: SurveyClient
        public ActionResult Index()
        {
            List<SurveyVM> surveysvm = new List<SurveyVM>();
            IEnumerable<Survey> surveys = surveyservice.GetMany(s => s.employeId!= 1 && s.status == true);

            List<Survey> surveyslist = new List<Survey>();
            if (surveys.Count() == 1)
                surveyslist.Add(surveys.First());
            else
            {
                foreach (var surv in surveys)
                {

                    surveyslist.Add(new Survey
                    {
                        date = surv.date,
                        duree = surv.duree,
                        //employe = surv.employe,
                        employeId = surv.employeId,
                        //manager = surv.manager,
                        managerId = surv.managerId,
                        status = surv.status,
                        // SurveyQuestions = surv.SurveyQuestions
                    });
                }
            }
            foreach (var survey in surveyslist)
            {

                if (DateTime.Compare(survey.date.Value.AddDays(survey.duree), DateTime.Now) > 0 )
                {

                    surveysvm.Add(new SurveyVM
                    {
                        date = survey.date,
                        duree = survey.duree,
                        employe_id = survey.employeId,
                        manager_id = survey.managerId,
                        employee_Name = us.GetById((int)survey.employeId).nom + "  " + us.GetById((int)survey.employeId).prenom,
                        status = true ? "Underway" : "Off"
                    });

                }
                else
                {
                    Survey su =surveyservice.Get(s => s.date == survey.date && s.employeId == survey.employeId && s.managerId == survey.managerId);
                    su.status = false;
                    surveyservice.Update(su);
                    surveyservice.Commit();
                }
            }


            return View(surveysvm);

        }

        // GET: SurveyClient/Details/5
        public ActionResult Details(long empid ,long manid,DateTime date)
        {
            Survey svy = surveyservice.Get(s => s.date == date && s.employeId == empid && s.managerId == manid);

            SurveyVM suvm = new SurveyVM
            {
                date = svy.date,
                duree = svy.duree,
                employe_id = svy.employeId,
                manager_id = svy.managerId,
                employee_Name = us.GetById((int)svy.employeId).nom + "  " + us.GetById((int)svy.employeId).prenom,
                status = "Underway"
            };

            ViewData["questions"] = svy.SurveyQuestions.ToList();

            return View(suvm);
        }

        // GET: SurveyClient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SurveyClient/Create
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

        // GET: SurveyClient/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SurveyClient/Edit/5
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

        // GET: SurveyClient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SurveyClient/Delete/5
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

        [HttpGet]
        public ActionResult UpdateQuestion(int questid, int appreciation)
        {
            SurveyQuestEmploye sq = surveyquestempservice.Get(s => s.surveyQuestId == questid && s.employeId == 1);
            if (sq == null) {
                surveyquestempservice.Add(new SurveyQuestEmploye() { comment = (SurveyQuestEmploye.SurveyComment)appreciation, employeId = 1, surveyQuestId = questid });
            surveyquestempservice.Commit();
                 }
            else { 
               
                sq.comment = (SurveyQuestEmploye.SurveyComment)appreciation;
                surveyquestempservice.Update(sq);
                surveyquestempservice.Commit();
            }
            
            
            return Json("fdasdfas", JsonRequestBehavior.AllowGet);

        }
    }
}
