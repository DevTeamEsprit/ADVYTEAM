namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("publication")]
    public partial class publication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public publication()
        {
            commentaires = new HashSet<commentaire>();
        }

        public long id { get; set; }

        public DateTime? dateCreation { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public long? id_user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<commentaire> commentaires { get; set; }

        public virtual utilisateur utilisateur { get; set; }
    }
}
