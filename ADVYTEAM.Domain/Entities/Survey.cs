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
    [Table("survey")]
    public class Survey
    {
        public Survey()
        {
            SurveyQuestions = new HashSet<SurveyQuestion>();
        }

        // public int Id { get; set; }

        [Key, Column(Order = 0)]
        public long managerId { get; set; }
        [Key, Column(Order = 1)]
        public long employeId { get; set; }
        public int duree { get; set; }

        [Column(TypeName = "bit")]
        public bool status { get; set; }
        [Key, Column(Order = 2)]
        public DateTime? date { get; set; }

        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }

        [ForeignKey("managerId")]
        public virtual utilisateur manager { get; set; }

        [ForeignKey("employeId")]
        public virtual utilisateur employe { get; set; }

    }
}
