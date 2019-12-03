using ADVYTEAM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Pattern
{
    public interface IFeedbackService: IService<userfeedback>
    {
        IEnumerable<userfeedback> getFeedbacksByQuizId(long quizId);
    }
}
