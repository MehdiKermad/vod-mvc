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
    public class DirectorsController : Controller
    {
        private VODContext db = new VODContext();

        // GET: Directors
        public ActionResult Index(int? id)
        {
            if (id == null || id < 0)
            {
                ViewBag.pageDirectors = 0;
            }
            else
            {
                ViewBag.pageDirectors = id;
            }

            var directors = db.Directors.OrderBy(d => d.LastName);

            if (TempData["msg"] != null)
            {
                ViewBag.msg = TempData["msg"].ToString();
                ViewBag.msgType = TempData["msgType"].ToString();
            }
            return View(directors);
        }

        public ActionResult Search()
        {

            List<string> nationalities = db.Directors.Select(f => f.Nationality).ToList();
            nationalities.Add("");
            nationalities.Sort();
            ViewBag.listeNationalities = nationalities.Distinct();

            return View();
        }

        public JsonResult Resultats(SearchMemberViewModel rech) //effectue le tri en fonction des critères
        {
            IQueryable<Director> directors = db.Directors;

            if (!String.IsNullOrWhiteSpace(rech.FirstName))
            {
                directors = directors.Where(f => f.FirstName.Contains(rech.FirstName));
            }
            if (!String.IsNullOrWhiteSpace(rech.LastName))
            {
                directors = directors.Where(f => f.LastName.Contains(rech.LastName));
            }
            if (!String.IsNullOrWhiteSpace(rech.AddNaissDate))
            {
                var dateNaiss = DateTime.ParseExact(rech.AddNaissDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                directors = directors.Where(f => f.Birth >= dateNaiss);
            }
            if (!String.IsNullOrWhiteSpace(rech.Nationality))
            {
                directors = directors.Where(f => f.Nationality == rech.Nationality);
            }

            return Json(directors.ToList());
        }

        // GET: Directors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director director = db.Directors.Find(id);

            if (director == null)
            {
                return HttpNotFound();
            }
                return View(director);
            
        }

        // GET: Directors/Create
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

        // POST: Directors/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nationality,FirstName,LastName,Birth")] Director director)
        {
            if (ModelState.IsValid)
            {
                db.Directors.Add(director);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(director);
        }

        // GET: Directors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director director = db.Directors.Find(id);
            if (director == null)
            {
                return HttpNotFound();
            }
            if ((String)Session["LoginAdmin"] == "True")
            {
                return View(director);
            }
            else
            {
                return RedirectToAction("../Films");
            }
        }

        // POST: Directors/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nationality,FirstName,LastName,Birth")] Director director)
        {
            if (ModelState.IsValid && (String)Session["LoginAdmin"] == "True")
            {
                db.Entry(director).State = EntityState.Modified;
                db.SaveChanges();

                if (Request.Files.Count > 0) //sauvegarde de la jacket si elle a été envoyée
                {
                    var jack = Request.Files[0];

                    if (jack != null && jack.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/Content/Images/Directors/"), director.Id + ".jpg");
                        jack.SaveAs(path);
                    }
                }
                return RedirectToAction("Index");
            }
            return View(director);
        }

        // GET: Directors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director director = db.Directors.Find(id);
            if (director == null)
            {
                return HttpNotFound();
            }
            if ((String)Session["LoginAdmin"] == "True")
            {
                return View(director);
            }
            else
            {
                return RedirectToAction("../Films");
            }
        }

        // POST: Directors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Director director = db.Directors.Find(id);
            db.Directors.Remove(director);
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
