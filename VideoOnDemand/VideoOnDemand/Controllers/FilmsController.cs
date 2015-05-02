using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.DAL;
using VideoOnDemand.Models;
using VideoOnDemand.ModelViews;

namespace VideoOnDemand.Controllers
{
    public class FilmsController : Controller
    {
        private VODContext db = new VODContext();

        // GET: Films
        public ActionResult Index(int? id)
        {
            if(id == null || id < 0)
            {
                ViewBag.pageFilms = 0;
            }
            else
            {
                ViewBag.pageFilms = id;
            }

            var films = db.Films.OrderBy(film => film.Ajout);
            films.Reverse();

            return View(films);
        }

        public ActionResult Search()
        {
            return View(db.Films);
        }

        public ActionResult Test(Search recherche)
        {
            return View(recherche);
        }

        // GET: Films/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // GET: Films/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Theme,Description,Sortie")] Film film, HttpPostedFileBase jacket)
        {
            if (ModelState.IsValid)
            {
                film.Ajout = DateTime.Now;
                db.Films.Add(film);
                db.SaveChanges();

                if (Request.Files.Count > 0) //sauvegarde de la jacket si elle a été envoyée
                {
                    var jack = Request.Files[0];

                    if (jack != null && jack.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/Content/Images/Jackets/"), film.Id + ".jpg");
                        jack.SaveAs(path);
                    }
                }

                return RedirectToAction("Index");
            }

            return View(film);
        }

        // GET: Films/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Theme,Description,Sortie,Ajout")] Film film, HttpPostedFileBase jacket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();

                if (Request.Files.Count > 0) //sauvegarde de la jacket si elle a été envoyée
                {
                    var jack = Request.Files[0];

                    if (jack != null && jack.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/Content/Images/Jackets/"), film.Id + ".jpg");
                        jack.SaveAs(path);
                    }
                }

                return RedirectToAction("Index");
            }
            return View(film);
        }

        // GET: Films/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Film film = db.Films.Find(id);
            db.Films.Remove(film);
            db.SaveChanges();

            var path = Path.Combine(Server.MapPath("~/Content/Images/Jackets/"), film.Id + ".jpg");
            if (System.IO.File.Exists(path)) //si une jacket existe on l'efface
            {
                System.IO.File.Delete(path);
            }

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
