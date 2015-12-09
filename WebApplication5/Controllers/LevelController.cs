using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Filters;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class LevelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: LevelModels
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var levelModels = db.LevelModels.Include(l => l.Difficulty);
            return View(levelModels.ToList());
        }
        [Authorize]
        [TimeFilter]
        public ActionResult Question()
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var levelId = db.LevelModels.FirstOrDefault(x => x.Question == "Complete").LevelId;
            if (levelId == user.LevelId)
                return RedirectToAction("Feedback", "Home");
            LevelModel levelModel = db.LevelModels.Find(user.LevelId);
            levelModel.Answer = string.Empty;
            return View(levelModel);
        }


        [Authorize]
        [HttpPost]
        [TimeFilter]
        public ActionResult Question(LevelModel model)
        {
            if (ModelState.IsValid)
            {
                var actual = model.Answer;
                var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                var expected = db.LevelModels.Find(user.LevelId).Answer;
                if (string.Equals(actual.Trim(), expected, StringComparison.OrdinalIgnoreCase))
                {
                    int? levelId = db.LevelModels?.OrderBy(x => x.LevelId)?.FirstOrDefault(x => x.LevelId > user.LevelId)?.LevelId ;                    
                    user.LevelId = levelId.Value;
                    user.Updated = DateTime.Now;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();                    
                }
            }
            ModelState.Clear();
            return RedirectToAction("Question", "Level");
        }

       
        // GET: LevelModels/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LevelModel levelModel = db.LevelModels.Find(id);
            if (levelModel == null)
            {
                return HttpNotFound();
            }
            return View(levelModel);
        }

        // GET: LevelModels/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.DifficultyId = new SelectList(db.DifficultyModels, "Id", "Description");
            return View();
        }

        // POST: LevelModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Question,Answer,DifficultyId")] LevelModel levelModel)
        {
            if (ModelState.IsValid)
            {
                db.LevelModels.Add(levelModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DifficultyId = new SelectList(db.DifficultyModels, "Id", "Description", levelModel.DifficultyId);
            return View(levelModel);
        }

        // GET: LevelModels/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LevelModel levelModel = db.LevelModels.Find(id);
            if (levelModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.DifficultyId = new SelectList(db.DifficultyModels, "Id", "Description", levelModel.DifficultyId);
            return View(levelModel);
        }

        // POST: LevelModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Question,Answer,DifficultyId")] LevelModel levelModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(levelModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DifficultyId = new SelectList(db.DifficultyModels, "Id", "Description", levelModel.DifficultyId);
            return View(levelModel);
        }

        // GET: LevelModels/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LevelModel levelModel = db.LevelModels.Find(id);
            if (levelModel == null)
            {
                return HttpNotFound();
            }
            return View(levelModel);
        }

        // POST: LevelModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            LevelModel levelModel = db.LevelModels.Find(id);
            db.LevelModels.Remove(levelModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
