using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADVYTEAM.Presentation.Models
{
    public class UserVM
    {
        public long id { get; set; }
        public string image { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
    }
}