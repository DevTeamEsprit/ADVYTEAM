namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ticket")]
    public partial class ticket
    {
        public int id { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public DateTime? endDate { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public DateTime? startDate { get; set; }

        [StringLength(255)]
        public string status { get; set; }

        public long? employe_id { get; set; }

        public int? project_id { get; set; }

        public virtual project project { get; set; }

        public virtual utilisateur utilisateur { get; set; }
    }
}
