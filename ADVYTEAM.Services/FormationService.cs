using ADVYTEAM.Data;
using ADVYTEAM.Data.Infrastructure;
using Service.Pattern;

namespace ADVYTEAM.Services
{
    public class FormationService : Service<formation> , IFormationService
    {
        static IDataBaseFactory factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(factory);
        public FormationService():base(utk)
        {

        }
    }
}
