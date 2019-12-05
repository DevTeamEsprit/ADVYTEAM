namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("message")]
    public partial class message
    {
        [Key]
        public long idMessage { get; set; }

        [StringLength(16777215)]
        public string content { get; set; }

        public DateTime? date { get; set; }

        public long? id_receiver { get; set; }

        public long? id_sender { get; set; }

        public virtual utilisateur sender { get; set; }

        public virtual utilisateur receiver { get; set; }
    }
}
