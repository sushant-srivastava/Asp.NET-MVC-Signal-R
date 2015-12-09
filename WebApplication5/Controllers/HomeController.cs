using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Filters;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        [TimeFilter]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        [TimeFilter]
        public ActionResult LeaderBoard()
        {
            ViewBag.Message = "Top 5 Users are";

            return View();
        }

        [Authorize]
        [TimeFilter]
        public ActionResult Feedback()
        {
            ViewBag.Message = "Thank you for your time, please give us your valuable feedback";
            return View();
        }

        [Authorize]
        [HttpPost]
        [TimeFilter]
        public ActionResult Feedback(FeedbackModel model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.FeedBackModels.Add(new FeedbackModel() { Comments = model.Comments });
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Wait()
        {
            return View();
        }
    }
}