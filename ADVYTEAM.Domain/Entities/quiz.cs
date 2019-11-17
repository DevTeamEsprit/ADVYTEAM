namespace ADVYTEAM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("quiz")]
    public partial class quiz
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quiz()
        {
            userquizresponses = new HashSet<userquizresponse>();
            userquizs = new HashSet<userquiz>();
            userfeedbacks = new HashSet<userfeedback>();
            quizquestions = new HashSet<quizquestion>();
        }

        public long id { get; set; }

        public int? required_min_level { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        public long? skill_id { get; set; }

        public int? min_correct_questions_percentage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userquizresponse> userquizresponses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userquiz> userquizs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userfeedback> userfeedbacks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quizquestion> quizquestions { get; set; }

        public virtual skill skill { get; set; }
    }
}
