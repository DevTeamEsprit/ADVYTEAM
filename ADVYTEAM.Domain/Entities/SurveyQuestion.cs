using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVYTEAM.Domain.Entities
{
    [Table("surveyquestion")]
    public class SurveyQuestion
    {
        [Key]
        public int id { get; set; }
        public string question { get; set; }
        public long managerId { get; set; }
        public long employeId { get; set; }
        public DateTime? date { get; set; }

        [ForeignKey("managerId,employeId,date")]
        public virtual Survey survey { get; set; }
    }
}
