using ADVYTEAM.Data.Infrastructure;
using ADVYTEAM.Domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ADVYTEAM.Domain.Entities.SurveyQuestEmploye;

namespace ADVYTEAM.Services
{
    public class SurveyQuestEmpService : Service<SurveyQuestEmploye>, ISurveyQuestEmpService
    {
        static IDataBaseFactory factory = new DataBaseFactory();
    static IUnitOfWork utk = new UnitOfWork(factory);

    public SurveyQuestEmpService() : base(utk)
    {

    }

        public int QuestionStats(int idquest , SurveyComment comment) {
           /* int nbrq = GetMany(q => q.surveyQuestId == idquest && q.comment == comment).Count();
            int nbrt = GetMany(q => q.surveyQuestId == idquest).Count();
            if (nbrq == 0 || nbrt == 0)
                return 0;

            double result = 1 / ( nbrt / nbrq ) *100d;*/
            return GetMany(q => q.surveyQuestId == idquest && q.comment == comment).Count(); ;
        }
    }
}
