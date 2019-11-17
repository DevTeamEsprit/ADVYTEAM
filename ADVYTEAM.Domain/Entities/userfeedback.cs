namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("userfeedback")]
    public partial class userfeedback
    {
        public int id { get; set; }

        [StringLength(255)]
        public string feedback { get; set; }

        public long? quiz_id { get; set; }

        public long? user_id { get; set; }

        public virtual quiz quiz { get; set; }

        public virtual utilisateur utilisateur { get; set; }
    }
}
