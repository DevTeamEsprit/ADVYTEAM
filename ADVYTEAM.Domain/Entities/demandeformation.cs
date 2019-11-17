namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("demandeformation")]
    public partial class demandeformation
    {
        public int id { get; set; }

        public DateTime? demande { get; set; }

        public int? id_formation { get; set; }

        public long? id_user { get; set; }

        public virtual utilisateur utilisateur { get; set; }

        public virtual formation formation { get; set; }
    }
}
