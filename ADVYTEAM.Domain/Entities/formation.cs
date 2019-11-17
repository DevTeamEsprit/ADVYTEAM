namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("formation")]
    public partial class formation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public formation()
        {
            demandeformations = new HashSet<demandeformation>();
        }

        public int id { get; set; }

        public DateTime? dateformation { get; set; }

        [StringLength(255)]
        public string decription { get; set; }

        public int nbrjour { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<demandeformation> demandeformations { get; set; }
    }
}
