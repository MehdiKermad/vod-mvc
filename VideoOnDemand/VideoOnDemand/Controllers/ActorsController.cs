using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Entity;
using VideoOnDemand.Models;
using VideoOnDemand.ModelViews;

namespace VideoOnDemand.Controllers
{
    public class ActorsController : Controller
    {
        private VODContext db = new VODContext();

        // GET: Actors
        public ActionResult Index(int? id)
        {
            if (id == null || id < 0)
            {
                ViewBag.pageActors = 0;
            }
            else
            {
                ViewBag.pageActors = id;
            }

            var actors = db.Actors.OrderBy(actor => actor.LastName);

            if (TempData["msg"] != null)
            {
                ViewBag.msg = TempData["msg"].ToString();
                ViewBag.msgType = TempData["msgType"].ToString();
            }
            return View(actors);
        }

        public ActionResult ListeActor()
        {
            var actors = db.Actors.OrderBy(actor => actor.LastName);
            actors.Reverse();

            return View(actors);
        }

        public ActionResult Search()
        {

            List<string> nationalities = db.Actors.Select(f => f.Nationality).ToList();
            nationalities.Add("");
            nationalities.Sort();
            ViewBag.listeNationalities = nationalities.Distinct();
            
            return View();
        }

        public JsonResult Resultats(SearchMemberViewModel rech) //effectue le tri en fonction des critères
        {
            IQueryable<Actor> actors = db.Actors;

            if (!String.IsNullOrWhiteSpace(rech.FirstName))
            {
                actors = actors.Where(f => f.FirstName.Contains(rech.FirstName));
            }
            if (!String.IsNullOrWhiteSpace(rech.LastName))
            {
                actors = actors.Where(f => f.LastName.Contains(rech.LastName));
            }
            if (!String.IsNullOrWhiteSpace(rech.AddNaissDate))
            {
                var AddNaissDate = DateTime.ParseExact(rech.AddNaissDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                actors = actors.Where(f => f.Birth >= AddNaissDate);
            }
            if (!String.IsNullOrWhiteSpace(rech.Biography))
            {
                actors = actors.Where(f => f.Biography == rech.Biography);
            }
            if (!String.IsNullOrWhiteSpace(rech.Nationality))
            {
                actors = actors.Where(f => f.Nationality == rech.Nationality);
            }

            return Json(actors.ToList());
        }

        // GET: Actors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);

            if (actor == null)
            {
                return HttpNotFound();
            }
            
            return View(actor);
       
        }

        // GET: Actors/Create
        public ActionResult Create()
        {
            if ((String)Session["LoginAdmin"] == "True")
            {
                return View();
            }
            else
            {
                return RedirectToAction("../Films");
            }
        }

        // POST: Actors/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nationality,Biography,FirstName,LastName,Birth")] Actor actor)
        {
            if (ModelState.IsValid && (String)Session["LoginAdmin"] == "True")
            {
                db.Actors.Add(actor);
                db.SaveChanges();

                if (Request.Files.Count > 0) //sauvegarde de la jacket si elle a été envoyée
                {
                    var jack = Request.Files[0];

                    if (jack != null && jack.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/Content/Images/Actors/"), actor.Id + ".jpg");
                        jack.SaveAs(path);
                    }
                }
                return RedirectToAction("Index");
            }

            return View(actor);
        }

        // GET: Actors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            if ((String)Session["LoginAdmin"] == "True")
            {
                return View(actor);
            }
            else
            {
                return RedirectToAction("../Films");
            }
        }

        // POST: Actors/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nationality,Biography,FirstName,LastName,Birth")] Actor actor)
        {
            if (ModelState.IsValid && (String)Session["LoginAdmin"] == "True")
            {
                db.Entry(actor).State = EntityState.Modified;
                db.SaveChanges();

                if (Request.Files.Count > 0) //sauvegarde de la jacket si elle a été envoyée
                {
                    var jack = Request.Files[0];

                    if (jack != null && jack.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/Content/Images/Actors/"), actor.Id + ".jpg");
                        jack.SaveAs(path);
                    }
                }
                return RedirectToAction("Index");
            }
            return View(actor);
        }

        // GET: Actors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            if ((String)Session["LoginAdmin"] == "True")
            {
                return View(actor);
            }
            else
            {
                return RedirectToAction("../Films");
            }
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Actor actor = db.Actors.Find(id);
            db.Actors.Remove(actor);
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
