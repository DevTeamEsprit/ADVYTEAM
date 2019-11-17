namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("commentaire")]
    public partial class commentaire
    {
        public long id { get; set; }

        public DateTime? dateCreation { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public long? id_pub { get; set; }

        public long? id_user { get; set; }

        public virtual utilisateur utilisateur { get; set; }

        public virtual publication publication { get; set; }
    }
}
