using ADVYTEAM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVYTEAM.Domain.Entities
{
    [Table("surveyquestemploye")]
    public class SurveyQuestEmploye
    {
        public enum SurveyComment { BAD, MEDIUM, GOOD, EXCELLENT };

        
        public long employeId { get; set; }
      
        public int surveyQuestId { get; set; }
        [Key]
        public int id { get; set; }
        public SurveyComment comment { get; set; }

        [ForeignKey("surveyQuestId")]
        public virtual SurveyQuestion SurveyQuestion { get; set; }

        [ForeignKey("employeId")]
        public virtual utilisateur employe { get; set; }
    }
}
