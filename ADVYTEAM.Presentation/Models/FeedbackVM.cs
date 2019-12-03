using ADVYTEAM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADVYTEAM.Presentation.Models
{
    public class FeedbackVM
    {
        public string feedback { get; set; }
        public userquiz Userquiz { get; set; }
    }
}