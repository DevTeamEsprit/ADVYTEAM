using ADVYTEAM.Data;
using ADVYTEAM.Data.Infrastructure;
using ADVYTEAM.Domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVYTEAM.Services
{
    public class ReclamationService : Service<reclamation>, IReclamationService
    {
      
        static IDataBaseFactory factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(factory);

        public ReclamationService() : base(utk)
        {

        }

         
    }
}
