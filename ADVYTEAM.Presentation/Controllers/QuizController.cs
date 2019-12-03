using ADVYTEAM.Data;
using ADVYTEAM.Presentation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ADVYTEAM.Presentation.Controllers
{
    public class QuizController : Controller
    {
        readonly HttpClient Client = new HttpClient();
        QuizSelectionVM QsVm;
        QuizSessionVM QpVm;
        long userId;

        public QuizController()
        {
            this.Client.BaseAddress = new Uri("http://localhost:9080/PIDEV-web/");
            this.Client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            QsVm = new QuizSelectionVM();
            QpVm = new QuizSessionVM();
        }

        public ActionResult Index()
        {
            QsVm = new QuizSelectionVM();

            HttpClient Client = new HttpClient();
            HttpResponseMessage response;
            Client.BaseAddress = new Uri("http://localhost:9080/PIDEV-web/");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            response = Client.GetAsync("PIDEV/gestionQuiz/category").Result;

            Console.WriteLine(response);

            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();

            List<Data.category> cats = new List<Data.category>() { new Data.category() { name = "MyCat" } };

            var x = jsonString.Result;
            cats = JsonConvert.DeserializeObject<List<Data.category>>(jsonString.Result);

            ViewBag.cats = new SelectList(cats, "id", "name"); ;

            // Prepare dropdown for categories...
            IEnumerable<SelectListItem> items = cats.Select(c => new SelectListItem
            {
                Value = c.id.ToString(),
                Text = c.name

            });
           
            QsVm.dropDownCategories = items;

            if (ModelState.IsValid)
            {
                Console.WriteLine("SELECTED CATEGORY: " + QsVm.SelectedCategory);
                //check for model.SelectedCountry property value here
                //Save and Redirect
            }

            return View();
        }

        [HttpGet]
        public ActionResult UpdateSkills(string categoryId)
        {
            Console.WriteLine("UpdateSkills called with: " + categoryId);

            Task<string> task = ReadAsStringAsync("PIDEV/gestionQuiz/skill/" + categoryId);
            task.Wait();

            string responseStr = task.Result;

            List<skill> Skills = JsonConvert.DeserializeObject<List<skill>>(responseStr);
            IEnumerable<SelectListItem> SkillItems = Skills.Select(c => new SelectListItem
            {
                Value = c.id.ToString(),
                Text = c.name

            });

            QsVm.SkillItems = SkillItems;

            return Json(SkillItems, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SetSelectedSkill(string skillId, string name = null)
        {
            if (skillId == null || skillId == String.Empty)
                return Json("Please select a valid skill.", JsonRequestBehavior.AllowGet);


            QsVm.SelectedSkill = skillId;

            return Json("Selecting " + (name ?? "skill ") + "done.", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Start Quiz submit button.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult QuizSelection(QuizSelectionVM InQsVm)
        {
            Console.WriteLine(InQsVm);
            return RedirectToAction("LoadQuizSession", "QuizSession", new { skillId = InQsVm.SelectedSkill });
        }
       
        public ActionResult QuizSelection()
        {
            Task<string> task = ReadAsStringAsync("PIDEV/gestionQuiz/category");
            task.Wait();

            string responseStr = task.Result;
            
            List<category> Categories = JsonConvert.DeserializeObject<List<category>>(responseStr);
            IEnumerable<SelectListItem> CategoryItems = Categories.Select(c => new SelectListItem
            {
                Value = c.id.ToString(),
                Text = c.name

            });

            QsVm.CategoryItems = CategoryItems;

            IEnumerable<SelectListItem> SkillItems = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = "Select a skill"
                }
            };

            QsVm.SkillItems = SkillItems;

            return View(QsVm);
        }

        #region Helpers
        private Task<string> ReadAsStringAsync(string RequestUrl)
        {
            return Client.GetAsync(RequestUrl).Result.Content.ReadAsStringAsync();
        }

        #endregion
    }
}
