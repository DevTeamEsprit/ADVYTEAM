namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("questionresponse")]
    public partial class questionresponse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public questionresponse()
        {
            userquizresponses = new HashSet<userquizresponse>();
        }

        [NotMapped]
        public bool isChecked = false;

        public int id { get; set; }

        [StringLength(255)]
        public string content { get; set; }

        [Column(TypeName = "bit")]
        public bool isCorrect { get; set; }

        public int? question_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userquizresponse> userquizresponses { get; set; }

        public virtual quizquestion quizquestion { get; set; }
    }
}
