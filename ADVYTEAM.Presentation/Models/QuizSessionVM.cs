using ADVYTEAM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADVYTEAM.Presentation.Models
{
    public class QuizSessionVM
    {
        // Selection Properties
        public string question { get; set; }
        public string response { get; set; }
        public userquiz selectedQuiz { get; set; }

        public List<userquizresponse> userResponses { get; set; }

        // Enumerables
        public IEnumerable<SelectListItem> QuizItems { get; set; }
        public List<questionresponse> ResponseItems { get; set; }
        public List<quizquestion> QuestionsItems { get; set; }

        // If false: show finish
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }

        public int skillId { get; set; }

    }
}