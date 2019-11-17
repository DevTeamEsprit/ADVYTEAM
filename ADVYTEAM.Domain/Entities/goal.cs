namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("goal")]
    public partial class goal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public goal()
        {
            goalbyemployes = new HashSet<goalbyemploye>();
        }

        public int id { get; set; }

        [StringLength(255)]
        public string text { get; set; }

        public int? type { get; set; }

        public int? evaluation_id { get; set; }

        public virtual evaluation evaluation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<goalbyemploye> goalbyemployes { get; set; }
    }
}
