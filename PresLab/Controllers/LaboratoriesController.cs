using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PresLab.Models;

namespace PresLab.Controllers
{
    public class LaboratoriesController : Controller
    {
        private LabPresContext db = new LabPresContext();

        // GET: Laboratories
        public ActionResult Index()
        {
            return View(db.Laboratories.ToList());
        }

        // GET: Laboratories/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratory laboratory = db.Laboratories.Find(id);
            if (laboratory == null)
            {
                return HttpNotFound();
            }
            return View(laboratory);
        }

        // GET: Laboratories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Laboratories/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,Cuit")] Laboratory laboratory)
        {
            if (ModelState.IsValid)
            {
                db.Laboratories.Add(laboratory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(laboratory);
        }

        // GET: Laboratories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratory laboratory = db.Laboratories.Find(id);
            if (laboratory == null)
            {
                return HttpNotFound();
            }
            return View(laboratory);
        }

        // POST: Laboratories/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Email,Cuit")] Laboratory laboratory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laboratory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(laboratory);
        }

        // GET: Laboratories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratory laboratory = db.Laboratories.Find(id);
            if (laboratory == null)
            {
                return HttpNotFound();
            }
            return View(laboratory);
        }

        // POST: Laboratories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Laboratory laboratory = db.Laboratories.Find(id);
            db.Laboratories.Remove(laboratory);
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
