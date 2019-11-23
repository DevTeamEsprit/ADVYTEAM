using ADVYTEAM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADVYTEAM.Presentation.Models
{
    public class PublicationVM
    {
        public string description { get; set; }

        public long? id_user { get; set; }
        public virtual utilisateur utilisateur { get; set; }
    }
    //public class OurModel
    //{
    //    public  PublicationVM aa { get;set;}
    //    public UserVM bb { get; set; }
    //}
}