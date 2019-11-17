using ADVYTEAM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVYTEAM.Domain.Entities
{
    public class reclamation
    {

        public int Id { get; set; }
        public string Objet { get; set; }
        public string Description { get; set; }
        public DateTime DateReclamation { get; set; }
        public DateTime? DateTraitement { get; set; }
        public bool Etat { get; set; }

        public long? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual utilisateur Utilisateur { get; set; }
    }
}
