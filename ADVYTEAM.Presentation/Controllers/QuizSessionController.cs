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

        readonly int minRequiredScore = 80;

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

            // No quiz for such level on this skill for this user...
            if(userQuiz == null)
                return RedirectToAction("QuizSelection", "Quiz");


            Task<string> task;
            String responseStr;

            if (incQIndex != 0)
            { 
                this.userQuiz.currentQuestionIndex = this.userQuiz.currentQuestionIndex + incQIndex;
                task = ReadAsStringAsync("PIDEV/gestionQuiz/updateQuestionIndex/" + this.userQuiz.id + "/" + this.userQuiz.currentQuestionIndex);
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
        
        public ActionResult FinishQuiz(int skillId)
        {
            this.userQuiz = GetUserQuizBySkillId(skillId);

            Task<string> task;
            String responseStr;

            QsessionVm.selectedQuiz = userQuiz;
            QsessionVm.skillId = skillId;

            float correctQuestionsCount = 0;
            float questionsCount = this.userQuiz.quiz.questions.Count();

            for (int qi = 0; qi < questionsCount; ++qi)
            {
                task = ReadAsStringAsync(
                    "PIDEV/gestionQuiz/getUserQuestionResponses/"
                    + this.userId + "/" + (int)this.userQuiz.quiz.questions.ElementAt(qi).id);
                task.Wait();
                responseStr = task.Result;
                List<userquizresponse> uqrs = JsonConvert.DeserializeObject<List<userquizresponse>>(responseStr);
                QsessionVm.userResponses = uqrs;

                bool isCorrect = true;

                for (int ri = 0; ri < uqrs.Count; ++ri)
                {
                    if (QsessionVm.selectedQuiz.quiz.questions.ElementAt(qi).responses.ElementAt(ri).isCorrect != uqrs[ri].isChecked)
                    {
                        isCorrect = false;
                        break;
                    }
                }

                if (isCorrect) ++correctQuestionsCount;
            }

            float correctAnswersPercentage = (int)(correctQuestionsCount / questionsCount) * 100;

            int newScore = (int)correctAnswersPercentage;

            task = ReadAsStringAsync("PIDEV/gestionQuiz/updateScore/" + this.userQuiz.quiz.id + "/" + userId + "/" + newScore);
                        task.Wait();
                        responseStr = task.Result;

            //updateScore/{quizId}/{userId}/{score}

            // Check if this percentage is enough to pass the quiz.
            ViewBag.score = 0.0f;
            ViewBag.requiredScore = this.minRequiredScore;

            if (newScore >= minRequiredScore)
            {
                // Now, depending on the user type, we update skill level...

                // Meaning an employee
                if (true)
                {
                    task = ReadAsStringAsync("PIDEV/gestionQuiz/levelUpUserSkill/" + userId + "/" + skillId);
                    task.Wait();
                    responseStr = task.Result;

                    ViewBag.score = newScore;

                    return View(QsessionVm);
                    //

                }
                else // Else, a candidate
                {
                    // Use calendar to schedule an meet-up

                }

            }

            return View(QsessionVm);
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


            quiz quiz = JsonConvert.DeserializeObject<quiz>(responseStr);

            if (quiz == null)
                return null;

            long quizId = quiz.id;

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
