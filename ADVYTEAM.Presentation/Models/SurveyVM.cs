using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADVYTEAM.Presentation.Models
{
    public class SurveyVM
    {

        public int Id { get; set; }

        public int duree { get; set; }

        public long manager_id { get; set; }
        public long employe_id { get; set; }

        public string status { get; set; }

        public DateTime? date { get; set; }

        public string employee_Name { get; set; }

    }
}