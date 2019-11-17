namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("project")]
    public partial class project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public project()
        {
            tickets = new HashSet<ticket>();
        }

        public int id { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public DateTime? endDate { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public DateTime? startDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ticket> tickets { get; set; }
    }
}
