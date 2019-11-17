namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("evaluation")]
    public partial class evaluation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public evaluation()
        {
            goals = new HashSet<goal>();
        }

        public int id { get; set; }

        public DateTime? date { get; set; }

        [Column(TypeName = "bit")]
        public bool status { get; set; }

        public int? type { get; set; }

        public long? manager_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<goal> goals { get; set; }

        public virtual utilisateur utilisateur { get; set; }
    }
}
