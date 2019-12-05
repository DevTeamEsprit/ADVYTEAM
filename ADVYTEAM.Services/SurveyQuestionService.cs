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
    public class SurveyQuestionService : Service<SurveyQuestion>, ISurveyQuestionService
    {
        static IDataBaseFactory factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(factory);

        public SurveyQuestionService() : base(utk)
        {

        }
    }
}
