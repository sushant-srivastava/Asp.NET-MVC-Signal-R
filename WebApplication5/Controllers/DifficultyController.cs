using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class DifficultyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DifficultyModels
        public ActionResult Index()
        {
            return View(db.DifficultyModels.ToList());
        }

        // GET: DifficultyModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DifficultyModel difficultyModel = db.DifficultyModels.Find(id);
            if (difficultyModel == null)
            {
                return HttpNotFound();
            }
            return View(difficultyModel);
        }

        // GET: DifficultyModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DifficultyModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description")] DifficultyModel difficultyModel)
        {
            if (ModelState.IsValid)
            {
                db.DifficultyModels.Add(difficultyModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(difficultyModel);
        }

        // GET: DifficultyModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DifficultyModel difficultyModel = db.DifficultyModels.Find(id);
            if (difficultyModel == null)
            {
                return HttpNotFound();
            }
            return View(difficultyModel);
        }

        // POST: DifficultyModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description")] DifficultyModel difficultyModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(difficultyModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(difficultyModel);
        }

        // GET: DifficultyModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DifficultyModel difficultyModel = db.DifficultyModels.Find(id);
            if (difficultyModel == null)
            {
                return HttpNotFound();
            }
            return View(difficultyModel);
        }

        // POST: DifficultyModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DifficultyModel difficultyModel = db.DifficultyModels.Find(id);
            db.DifficultyModels.Remove(difficultyModel);
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
