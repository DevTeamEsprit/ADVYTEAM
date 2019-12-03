using ADVYTEAM.Data;
using ADVYTEAM.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Pattern
{
    public class FeedbackService : Service<userfeedback>, IFeedbackService
    {
        static IDataBaseFactory factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(factory);

        public FeedbackService():base(utk)
        {

        }
        public IEnumerable<userfeedback> getFeedbacksByQuizId(long quizId)
        {
            return GetMany(f => f.quiz_id == quizId);
        }
    }
}
