namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("quizquestion")]
    public partial class quizquestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quizquestion()
        {
            questionresponses = new HashSet<questionresponse>();
        }

        public int id { get; set; }

        [StringLength(255)]
        public string content { get; set; }

        public long? quiz_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<questionresponse> questionresponses { get; set; }

        public virtual quiz quiz { get; set; }
    }
}
