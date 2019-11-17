namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("userquizresponse")]
    public partial class userquizresponse
    {
        public int id { get; set; }

        [Column(TypeName = "bit")]
        public bool isChecked { get; set; }

        public long? quiz_id { get; set; }

        public int? response_id { get; set; }

        public long? user_id { get; set; }

        public virtual questionresponse questionresponse { get; set; }

        public virtual quiz quiz { get; set; }

        public virtual utilisateur utilisateur { get; set; }
    }
}
