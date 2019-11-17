namespace ADVYTEAM.Data
{
    using ADVYTEAM.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("utilisateur")]
    public partial class utilisateur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public utilisateur()
        {
            commentaires = new HashSet<commentaire>();
            demandeformations = new HashSet<demandeformation>();
            evaluations = new HashSet<evaluation>();
            goalbyemployes = new HashSet<goalbyemploye>();
            messages = new HashSet<message>();
            messages1 = new HashSet<message>();
            publications = new HashSet<publication>();
            tickets = new HashSet<ticket>();
            userfeedbacks = new HashSet<userfeedback>();
            userquizs = new HashSet<userquiz>();
            userquizresponses = new HashSet<userquizresponse>();
            userskills = new HashSet<userskill>();
            utilisateur1 = new HashSet<utilisateur>();
        }

        [Required]
        [StringLength(31)]
        public string type { get; set; }

        public long id { get; set; }

        [Column(TypeName = "bit")]
        public bool Actif { get; set; }

        [StringLength(255)]
        public string adresse { get; set; }

        [StringLength(255)]
        public string cin { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [Column(TypeName = "bit")]
        public bool firstLogin { get; set; }

        [StringLength(16777215)]
        public string image { get; set; }

        [StringLength(255)]
        public string nom { get; set; }

        [StringLength(255)]
        public string password { get; set; }

        [StringLength(255)]
        public string prenom { get; set; }

        [StringLength(255)]
        public string sexe { get; set; }

        [StringLength(255)]
        public string tel { get; set; }

        public int? contrat_reference { get; set; }

        public long? manager_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? datNais { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<commentaire> commentaires { get; set; }

        public virtual contrat contrat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<demandeformation> demandeformations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<evaluation> evaluations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<goalbyemploye> goalbyemployes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<message> messages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<message> messages1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<publication> publications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ticket> tickets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userfeedback> userfeedbacks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userquiz> userquizs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userquizresponse> userquizresponses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userskill> userskills { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<utilisateur> utilisateur1 { get; set; }

        public virtual utilisateur utilisateur2 { get; set; }


        virtual public ICollection<reclamation> Reclamations { get; set; }
    }
}
