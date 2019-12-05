using ADVYTEAM.Data;
using ADVYTEAM.Domain.Entities;
using ADVYTEAM.Presentation.Models;
using ADVYTEAM.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ADVYTEAM.Presentation.Controllers
{
    public class SurveyController : Controller
    {
        readonly HttpClient Client = new HttpClient();
        static List<SurveyQuestion> questions = new List<SurveyQuestion>();
       
        UtilisateurService us = new UtilisateurService();
        SurveyService  surveyservice;
        ISurveyQuestionService surquestservice;
        SurveyQuestEmpService surveyquestempservice;

        public SurveyController()
        {
           this.Client.BaseAddress = new Uri("http://localhost:9080/PIDEV-web/");
            this.Client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            surveyservice = new SurveyService();
            surquestservice = new SurveyQuestionService();
            surveyquestempservice = new SurveyQuestEmpService();
        }
        // GET: Survey
        public ActionResult Index()
        {
            List<SurveyVM> surveysvm = new List<SurveyVM>();
            IEnumerable<Survey> surveys = surveyservice.GetSurveysByMan(2);

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
                surveysvm.Add(new SurveyVM
                {
                    date = survey.date,
                    duree = survey.duree,
                    employe_id = survey.employeId,
                    manager_id = survey.managerId,
                    employee_Name = us.GetById((int)survey.employeId).nom +"  " +us.GetById((int)survey.employeId).prenom,
                    status = true ? "Underway" : "Off"
                });

                if(DateTime.Compare(survey.date.Value.AddDays(survey.duree), DateTime.Now) < 0)
                {
                    Survey su = surveyservice.Get(s => s.date == survey.date && s.employeId == survey.employeId && s.managerId == survey.managerId);
                    su.status = false;
                    surveyservice.Update(su);
                    surveyservice.Commit();
                }
            }
            
           
            return View(surveysvm);

        
        }

        // GET: Survey/Details/5
        public ActionResult Details(long empid, long manid, DateTime date)
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

        // GET: Survey/Create
        public ActionResult Create()
        {
            //var dest = FormMethod.Get[]
           

            IEnumerable<utilisateur> employesnum = us.GetEmployesByman(2);
            List<utilisateur> employesList = new List<utilisateur>();
            foreach (var employe in employesnum)
            {
                employesList.Add(employe);
            }

                ViewData["employes"] = employesList;


            return View();
        }

        // POST: Survey/Create
        [HttpPost]
        public ActionResult Create(SurveyVM suv, string employename)
        {
            IEnumerable<utilisateur> employesnum = us.GetEmployesByman(2);
            List<utilisateur> employesList = new List<utilisateur>();
            foreach (var employe in employesnum)
            {
                employesList.Add(employe);
            }

            ViewData["employes"] = employesList;

            int empid = System.Convert.ToInt32(employename.Substring(0, 1));

            
           try
            {
                if (questions.Count() == 0)
                {
                    ModelState.AddModelError("", "Ajouter des questions !");
                    return View();
                }
                else
                {
                    foreach (SurveyQuestion quest in questions)
                    {
                        quest.date = DateTime.Now; quest.employeId = empid; quest.managerId = 2;
                        //surquestservice.Add(quest);
                    }

                    List<SurveyQuestion> questionlist = new List<SurveyQuestion>();
                    questionlist = questions;


                    foreach (SurveyQuestion quest in questionlist)
                    {
                        quest.date = DateTime.Now; quest.employeId = empid; quest.managerId = 2;
                        surquestservice.Add(quest);
                    }
                    Survey sv = new Survey() { date = DateTime.Now, duree = suv.duree, status = true, managerId = 2, employeId = empid };

                    surveyservice.Add(sv);
                    surveyservice.Commit();
                    surquestservice.Commit();
                    questions.Clear();

                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
               // Console.WriteLine(e.Message);
              
                return View(e.Message);
            }
        }

        // GET: Survey/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Survey/Edit/5
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

        // GET: Survey/Delete/5
        public ActionResult Delete(long empid, long manid, DateTime date)
        {
            //try { 
            Survey sur = surveyservice.Get(s => s.date == date && s.employeId == empid && s.managerId == manid);
            List<SurveyQuestion> surquests1 = new List<SurveyQuestion>();

            foreach (var surquest in sur.SurveyQuestions)
            {
                surquests1.Add(new SurveyQuestion
                {
                    id = surquest.id,
                    date = surquest.date,
                    //employe = surv.employe,
                    employeId = surquest.employeId,
                    //manager = surv.manager,
                    managerId = surquest.managerId,
                    // SurveyQuestions = surv.SurveyQuestions
                });
            }
            surquestservice.Commit();
            surveyservice.Delete(sur);
            foreach (var question in surquests1)
            {
                SurveyQuestion qs = surquestservice.Get(s => s.id == question.id);
                surquestservice.Delete(qs);
                surquestservice.Commit();
            }

            surveyservice.Delete(sur);

            
            surveyservice.Commit();

            return RedirectToAction("Index");
       /* }
             catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }*/
        }

        // POST: Survey/Delete/5
        [HttpPost]
        public ActionResult Delete( FormCollection collection)
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
        public ActionResult UpdateSkills(string userid)
        {
            Task<string> task = Client.GetAsync("PIDEV/evaluation/skill/"+userid.Substring(0,1)).Result.Content.ReadAsStringAsync();
            task.Wait();
            string responseStr = task.Result;

            List<skill> skills = JsonConvert.DeserializeObject<List<skill>>(responseStr);

     
            if (Request.IsAjaxRequest())
            return Json(skills, JsonRequestBehavior.AllowGet);

            return null;
        }

        [HttpGet]
        public ActionResult UpdateQuestions(string text)
        {
            
            questions.Add(new SurveyQuestion() {  question = text });

            //if (Request.IsAjaxRequest())
                return Json(questions, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult ClearQuestions()
        {

            questions.Clear();

            //if (Request.IsAjaxRequest())
            return Json(questions, JsonRequestBehavior.AllowGet);

        }

        public ActionResult StatByQuestion(int questid)
        {
            SurveyQuestion sq = surquestservice.Get(s =>s.id == questid);
            SurveyQuestionVM sqvm = new SurveyQuestionVM { Id =sq.id , question = sq.question};

            List<DataPoint> dataPoints = new List<DataPoint>();

            dataPoints.Add(new DataPoint("BAD", surveyquestempservice.QuestionStats(questid, SurveyQuestEmploye.SurveyComment.BAD)));
            dataPoints.Add(new DataPoint("MEDIUM", surveyquestempservice.QuestionStats(questid, SurveyQuestEmploye.SurveyComment.MEDIUM)));
            dataPoints.Add(new DataPoint("GOOD", surveyquestempservice.QuestionStats(questid, SurveyQuestEmploye.SurveyComment.GOOD)));
            dataPoints.Add(new DataPoint("EXCELLENT", surveyquestempservice.QuestionStats(questid, SurveyQuestEmploye.SurveyComment.EXCELLENT)));

            ViewData["datapoints"] = JsonConvert.SerializeObject(dataPoints);

            //if (Request.IsAjaxRequest())
            return View(sqvm);

        }
    }
}
