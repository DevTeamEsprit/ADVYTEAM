using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADVYTEAM.Presentation.Models
{
    public class oubliePass
    {
        public string email { get; set; }
        public int Code { get; set; }
        public string password { get; set; }
        public string confpassword { get; set; }
    }
}