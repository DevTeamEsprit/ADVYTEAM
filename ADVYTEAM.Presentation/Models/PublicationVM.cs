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
        public long id { get; set; }
        public DateTime? dateCreation { get; set; }
        public virtual ICollection<CommentaireVM> lstComm { get; set; }
        public virtual utilisateur user { get; set; }
    }
}