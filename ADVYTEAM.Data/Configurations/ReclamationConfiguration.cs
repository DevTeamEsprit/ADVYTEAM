using ADVYTEAM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVYTEAM.Data.Configurations
{
    class ReclamationConfiguration: EntityTypeConfiguration<reclamation>
    {
        public ReclamationConfiguration()
        {
            HasOptional(r => r.Utilisateur)
                .WithMany(u => u.Reclamations)
                .HasForeignKey(r => r.UserId)
                .WillCascadeOnDelete(true);
        }
    }
}
