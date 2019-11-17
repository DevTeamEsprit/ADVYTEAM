namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("evaluationsheet")]
    public partial class evaluationsheet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public evaluationsheet()
        {
            goalbyemployes = new HashSet<goalbyemploye>();
        }

        public int id { get; set; }

        [StringLength(255)]
        public string appreciation { get; set; }

        [StringLength(255)]
        public string evalsheetTitle { get; set; }

        [Column(TypeName = "bit")]
        public bool status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<goalbyemploye> goalbyemployes { get; set; }
    }
}
