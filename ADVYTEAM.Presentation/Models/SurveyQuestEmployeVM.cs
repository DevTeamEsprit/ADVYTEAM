using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ADVYTEAM.Domain.Entities.SurveyQuestEmploye;

namespace ADVYTEAM.Presentation.Models
{
    public class SurveyQuestEmployeVM
    {
        public int Id { get; set; }

        public SurveyComment comment { get; set; }
    }
}