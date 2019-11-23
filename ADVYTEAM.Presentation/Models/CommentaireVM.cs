using ADVYTEAM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADVYTEAM.Presentation.Models
{
    public class CommentaireVM
    {
        public long id { get; set; }
        public DateTime? dateCreation { get; set; }
        public string description { get; set; }

       // public long? id_user { get; set; }

        public virtual utilisateur user { get; set; }
    }
}