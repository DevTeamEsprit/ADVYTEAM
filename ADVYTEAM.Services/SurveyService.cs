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
    public class SurveyService :Service<Survey> , ISurveyService
    {

        static IDataBaseFactory factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(factory);

        public SurveyService() :base(utk)
        {

        }

        public void AddSurvey(Survey s) {
            Add(s);
           
            ISurveyQuestionService QS = new SurveyQuestionService();
            foreach(var question in s.SurveyQuestions){
                QS.Add(new SurveyQuestion() { date = s.date ,question = question.question ,employeId = s.employeId ,managerId = s.managerId});
               
            }
            // QS.Add(new SurveyQuestion());
            QS.Commit();
            Commit();
        }

        public IEnumerable<Survey> GetSurveysByMan(int manId)
        {
            return GetMany(s => s.managerId == manId);
        }
    }
}
