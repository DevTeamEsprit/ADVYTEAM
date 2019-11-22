namespace ADVYTEAM.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using ADVYTEAM.Data.Configurations;

    public partial class MyContext : DbContext
    {
        public MyContext()
            : base("name=MyContext1")
        {
            
        }

        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<commentaire> commentaires { get; set; }
        public virtual DbSet<contrat> contrats { get; set; }
        public virtual DbSet<demandeformation> demandeformations { get; set; }
        public virtual DbSet<evaluation> evaluations { get; set; }
        public virtual DbSet<evaluationsheet> evaluationsheets { get; set; }
        public virtual DbSet<formation> formations { get; set; }
        public virtual DbSet<goal> goals { get; set; }
        public virtual DbSet<goalbyemploye> goalbyemployes { get; set; }
        public virtual DbSet<message> messages { get; set; }
        public virtual DbSet<mission> missions { get; set; }
        public virtual DbSet<project> projects { get; set; }
        public virtual DbSet<publication> publications { get; set; }
        public virtual DbSet<questionresponse> questionresponses { get; set; }
        public virtual DbSet<quiz> quizs { get; set; }
        public virtual DbSet<quizquestion> quizquestions { get; set; }
        public virtual DbSet<skill> skills { get; set; }
        public virtual DbSet<ticket> tickets { get; set; }
        public virtual DbSet<userfeedback> userfeedbacks { get; set; }
        public virtual DbSet<userquiz> userquizs { get; set; }
        public virtual DbSet<userquizresponse> userquizresponses { get; set; }
        public virtual DbSet<userskill> userskills { get; set; }
        public virtual DbSet<utilisateur> utilisateurs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //***************************
            modelBuilder.Configurations.Add(new ReclamationConfiguration());
          
            //***************************
            modelBuilder.Entity<category>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .HasMany(e => e.skills)
                .WithOptional(e => e.category)
                .HasForeignKey(e => e.category_id);

            modelBuilder.Entity<commentaire>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<contrat>()
                .Property(e => e.typeContrat)
                .IsUnicode(false);

            modelBuilder.Entity<contrat>()
                .HasMany(e => e.utilisateurs)
                .WithOptional(e => e.contrat)
                .HasForeignKey(e => e.contrat_reference);

            modelBuilder.Entity<evaluation>()
                .HasMany(e => e.goals)
                .WithOptional(e => e.evaluation)
                .HasForeignKey(e => e.evaluation_id);

            modelBuilder.Entity<evaluationsheet>()
                .Property(e => e.appreciation)
                .IsUnicode(false);

            modelBuilder.Entity<evaluationsheet>()
                .Property(e => e.evalsheetTitle)
                .IsUnicode(false);

            modelBuilder.Entity<evaluationsheet>()
                .HasMany(e => e.goalbyemployes)
                .WithOptional(e => e.evaluationsheet)
                .HasForeignKey(e => e.evaluationSheet_id);

            modelBuilder.Entity<formation>()
                .Property(e => e.decription)
                .IsUnicode(false);

            modelBuilder.Entity<formation>()
                .HasMany(e => e.demandeformations)
                .WithOptional(e => e.formation)
                .HasForeignKey(e => e.id_formation);

            modelBuilder.Entity<goal>()
                .Property(e => e.text)
                .IsUnicode(false);

            modelBuilder.Entity<goal>()
                .HasMany(e => e.goalbyemployes)
                .WithRequired(e => e.goal)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<message>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<mission>()
                .Property(e => e.localisation)
                .IsUnicode(false);

            modelBuilder.Entity<project>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<project>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<project>()
                .HasMany(e => e.tickets)
                .WithOptional(e => e.project)
                .HasForeignKey(e => e.project_id);

            modelBuilder.Entity<publication>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<publication>()
                .HasMany(e => e.commentaires)
                .WithOptional(e => e.publication)
                .HasForeignKey(e => e.id_pub);

            modelBuilder.Entity<questionresponse>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<questionresponse>()
                .HasMany(e => e.userquizresponses)
                .WithOptional(e => e.questionresponse)
                .HasForeignKey(e => e.response_id);

            modelBuilder.Entity<quiz>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<quiz>()
                .HasMany(e => e.userquizresponses)
                .WithOptional(e => e.quiz)
                .HasForeignKey(e => e.quiz_id);

            modelBuilder.Entity<quiz>()
                .HasMany(e => e.userquizs)
                .WithOptional(e => e.quiz)
                .HasForeignKey(e => e.quiz_id);

            modelBuilder.Entity<quiz>()
                .HasMany(e => e.userfeedbacks)
                .WithOptional(e => e.quiz)
                .HasForeignKey(e => e.quiz_id);

            modelBuilder.Entity<quiz>()
                .HasMany(e => e.quizquestions)
                .WithOptional(e => e.quiz)
                .HasForeignKey(e => e.quiz_id);

            modelBuilder.Entity<quizquestion>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<quizquestion>()
                .HasMany(e => e.questionresponses)
                .WithOptional(e => e.quizquestion)
                .HasForeignKey(e => e.question_id);

            modelBuilder.Entity<skill>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<skill>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<skill>()
                .HasMany(e => e.quizs)
                .WithOptional(e => e.skill)
                .HasForeignKey(e => e.skill_id);

            modelBuilder.Entity<skill>()
                .HasMany(e => e.userskills)
                .WithOptional(e => e.skill)
                .HasForeignKey(e => e.skill_id);

            modelBuilder.Entity<ticket>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<ticket>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<ticket>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<userfeedback>()
                .Property(e => e.feedback)
                .IsUnicode(false);

            modelBuilder.Entity<utilisateur>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<utilisateur>()
                .Property(e => e.adresse)
                .IsUnicode(false);

            modelBuilder.Entity<utilisateur>()
                .Property(e => e.cin)
                .IsUnicode(false);

            modelBuilder.Entity<utilisateur>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<utilisateur>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<utilisateur>()
                .Property(e => e.nom)
                .IsUnicode(false);

            modelBuilder.Entity<utilisateur>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<utilisateur>()
                .Property(e => e.prenom)
                .IsUnicode(false);

            modelBuilder.Entity<utilisateur>()
                .Property(e => e.sexe)
                .IsUnicode(false);

            modelBuilder.Entity<utilisateur>()
                .Property(e => e.tel)
                .IsUnicode(false);

            modelBuilder.Entity<utilisateur>()
                .HasMany(e => e.commentaires)
                .WithOptional(e => e.utilisateur)
                .HasForeignKey(e => e.id_user);

            modelBuilder.Entity<utilisateur>()
                .HasMany(e => e.demandeformations)
                .WithOptional(e => e.utilisateur)
                .HasForeignKey(e => e.id_user);

            modelBuilder.Entity<utilisateur>()
                .HasMany(e => e.evaluations)
                .WithOptional(e => e.utilisateur)
                .HasForeignKey(e => e.manager_id);

            modelBuilder.Entity<utilisateur>()
                .HasMany(e => e.goalbyemployes)
                .WithRequired(e => e.utilisateur)
                .HasForeignKey(e => e.employeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<utilisateur>()
                .HasMany(e => e.messages)
                .WithOptional(e => e.utilisateur)
                .HasForeignKey(e => e.id_sender);

            modelBuilder.Entity<utilisateur>()
                .HasMany(e => e.messages1)
                .WithOptional(e => e.utilisateur1)
                .HasForeignKey(e => e.id_receiver);

            modelBuilder.Entity<utilisateur>()
                .HasMany(e => e.publications)
                .WithOptional(e => e.utilisateur)
                .HasForeignKey(e => e.id_user);

            modelBuilder.Entity<utilisateur>()
                .HasMany(e => e.tickets)
                .WithOptional(e => e.utilisateur)
                .HasForeignKey(e => e.employe_id);

            modelBuilder.Entity<utilisateur>()
                .HasMany(e => e.userfeedbacks)
                .WithOptional(e => e.utilisateur)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<utilisateur>()
                .HasMany(e => e.userquizs)
                .WithOptional(e => e.utilisateur)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<utilisateur>()
                .HasMany(e => e.userquizresponses)
                .WithOptional(e => e.utilisateur)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<utilisateur>()
                .HasMany(e => e.userskills)
                .WithOptional(e => e.utilisateur)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<utilisateur>()
                .HasMany(e => e.utilisateur1)
                .WithOptional(e => e.utilisateur2)
                .HasForeignKey(e => e.manager_id);
        }

        public System.Data.Entity.DbSet<ADVYTEAM.Domain.Entities.reclamation> reclamations { get; set; }
    }
}
