using ADVYTEAM.Data;
using ADVYTEAM.Presentation.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADVYTEAM.Presentation.Controllers
{
    public class FeedbackController : Controller
    {
        FeedbackVM feedbackVM;
        IFeedbackService service = null;
        public FeedbackController()
        {
            service = new FeedbackService();
            feedbackVM = new FeedbackVM();

        }
        // GET: Feedback
        public ActionResult Index()
        {
            return View();
        }




        // GET: Feedback/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Feedback/Create
        [HttpPost]
        public ActionResult Create(FeedbackVM feedbackvm)
        {
            userfeedback f = new userfeedback
            {
                feedback = feedbackvm.feedback,
                user_id = feedbackvm.Userquiz.user_id,
                quiz_id = feedbackvm.Userquiz.quiz_id
            };
            service.Add(f);
            service.Commit();
            return RedirectToAction("Index");

        }
    }
}
