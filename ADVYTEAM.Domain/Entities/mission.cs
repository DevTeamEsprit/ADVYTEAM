namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mission")]
    public partial class mission
    {
        public int id { get; set; }

        public int Duration { get; set; }

        public int Idemp { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        [StringLength(255)]
        public string localisation { get; set; }

        [Column(TypeName = "bit")]
        public bool resultat { get; set; }

        [Column(TypeName = "bit")]
        public bool stat { get; set; }
    }
}
