using ADVYTEAM.Data;
using ADVYTEAM.Presentation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ADVYTEAM.Presentation.Controllers
{
    public class QuizSessionController : Controller
    {
        readonly HttpClient Client = new HttpClient();
        QuizSessionVM QsessionVm;
        long userId;

        // Used to save current quiz quesitons...
        userquiz userQuiz;

        public QuizSessionController()
        {
            userId = 1;

            this.Client.BaseAddress = new Uri("http://localhost:9080/PIDEV-web/");
            this.Client.DefaultRequestHeaders.Accept.
                Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            QsessionVm = new QuizSessionVM();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadQuizSession(int skillId, int incQIndex = 0)
        {
            this.userQuiz = GetUserQuizBySkillId(skillId);
            Task<string> task;
            String responseStr;

            if (incQIndex != 0)
            { 
                this.userQuiz.currentQuestionIndex = this.userQuiz.currentQuestionIndex + incQIndex;
                task = ReadAsStringAsync("PIDEV/gestionQuiz/updateQuestionIndex" + this.userQuiz.id + "/" + this.userQuiz.currentQuestionIndex);
                task.Wait();
                responseStr = task.Result;
            }

            QsessionVm.selectedQuiz = userQuiz;
            QsessionVm.skillId = skillId;

            int? cqi = this.userQuiz.currentQuestionIndex;
            // Get userResponses
            //this.userQuiz.currentQuestionIndex = this.userQuiz.currentQuestionIndex + incQIndex;
            task = ReadAsStringAsync(
                "PIDEV/gestionQuiz/getUserQuestionResponses/"
                + this.userId + "/" + (int)this.userQuiz.quiz.questions.ElementAt(cqi != null ? cqi.Value : 0).id);
            task.Wait();
            responseStr = task.Result;
            List<userquizresponse> uqrs = JsonConvert.DeserializeObject<List<userquizresponse>>(responseStr);
            QsessionVm.userResponses = uqrs;

            for (int i = 0; i < uqrs.Count(); ++i)
            {
                QsessionVm.selectedQuiz.quiz.questions.ElementAt(cqi != null ? cqi.Value : 0).responses.ElementAt(i).isChecked = uqrs[i].isChecked;
            }

            // Setup has next & has previous buttons
            QsessionVm.HasNext = this.userQuiz.currentQuestionIndex + 1 < this.userQuiz.quiz.questions.Count();
            QsessionVm.HasPrevious = this.userQuiz.currentQuestionIndex != 0;


            return View("QuizSession", QsessionVm);
        }

        public ActionResult FinishQuiz()
        {
            return View();
        }

        public ActionResult SetResponseCheck(string responseId, string toChecked)
        {
            //update response
            Task<string> task = ReadAsStringAsync("PIDEV/gestionQuiz/updateResponse/" + userId + "/" + responseId + "/" + toChecked);
            task.Wait();
            string responseStr = task.Result;
            Console.WriteLine(responseStr);

            return Json(responseStr, JsonRequestBehavior.AllowGet);
        }

        #region Helpers
        private Task<string> ReadAsStringAsync(string RequestUrl)
        {
            return Client.GetAsync(RequestUrl).Result.Content.ReadAsStringAsync();
        }


        private userquiz GetUserQuizBySkillId(int skillId)
        {
            skillId = 1;

            //get QuizId
            Task<string> task = ReadAsStringAsync("PIDEV/gestionQuiz/quiz/" + skillId);
            task.Wait();
            string responseStr = task.Result;
            List<quiz> Quizzes = JsonConvert.DeserializeObject<List<quiz>>(responseStr);

            //get userSkill
            task = ReadAsStringAsync("PIDEV/gestionQuiz/userSkill/" + skillId + "/" + userId);
            task.Wait();
            responseStr = task.Result;
            userskill userSkill = JsonConvert.DeserializeObject<userskill>(responseStr);

            // getQuizOfSkillWithLevel
            task = ReadAsStringAsync("PIDEV/gestionQuiz/quizBySkillAndLevel/" + skillId + "/" + userSkill.level + 1);
            task.Wait();
            responseStr = task.Result;
            long quizId = JsonConvert.DeserializeObject<quiz>(responseStr).id;

            // Test on quizInfo nullability, because they might don't have a quiz with such level (like last level)

            //get relevant quiz
            // this.quizInfo = Quizzes.ElementAt(userSkill.level-1);



            //get userQuiz
            task = ReadAsStringAsync("PIDEV/gestionQuiz/quiz/" + quizId + "/" + userId);
            task.Wait();
            responseStr = task.Result;
            userquiz userQuiz = JsonConvert.DeserializeObject<userquiz>(responseStr);
            
            return userQuiz;
        }


        #endregion
    }

}
