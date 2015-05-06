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

namespace VideoOnDemand.Controllers
{
    public class UsersController : Controller
    {
        private VODContext db = new VODContext();

        // GET: Users
        public ActionResult Index()
        {
            if ((String)Session["LoginAdmin"] == "True")
            {
                var users = db.Users.ToList();
                return View(users);
            }
            else
            {
                TempData["msg"] = "Veuillez vous connecter";
                TempData["msgType"] = "alert-warning";

                return RedirectToAction("Index", "Films");
            }

        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if(id.HasValue)
            {
                if (((String)Session["LoginAdmin"] == "True") || ((String)Session["LoginUserID"] == id.ToString()))
                {
                    User user = db.Users.Find(id);
                    return View(user);
                }
            }

            TempData["msg"] = "Vous n'avez pas le droit de voir ce profil";
            TempData["msgType"] = "alert-warning";
            
            return RedirectToAction("Index", "Films");
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Pseudo,Mdp,Admin,FirstName,LastName,Birth")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                if (Request.Files.Count > 0) //sauvegarde de la jacket si elle a été envoyée
                {
                    var jack = Request.Files[0];

                    if (jack != null && jack.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/Content/Images/Users/"), user.Id + ".jpg");
                        jack.SaveAs(path);
                    }
                }
                Login(user); //l'utilisateur est directement connecté après inscription

                return RedirectToAction("Index", "Films");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if ((String)Session["LoginAdmin"] == "True" || (String)Session["LoginUserID"] == id.ToString())
                return View(user);
            else
            {
                TempData["msg"] = "Vous n'êtes pas autorisé à voir ce profil";
                TempData["msgType"] = "alert-danger";
                return RedirectToAction("Index", "Films");
            }
        }

        // POST: Users/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Pseudo,Mdp,Admin,FirstName,LastName,Birth")] User user)
        {
            if (ModelState.IsValid && (String)Session["LoginAdmin"] == "True")
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                if (Request.Files.Count > 0) //sauvegarde de la jacket si elle a été envoyée
                {
                    var jack = Request.Files[0];

                    if (jack != null && jack.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/Content/Images/Users/"), user.Id + ".jpg");
                        jack.SaveAs(path);
                    }
                }

                return RedirectToAction("Index", "Films");
            }

            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if ((String)Session["LoginAdmin"] == "True" || (String)Session["LoginUserID"] == id.ToString())
                return View(user);
            else
            {
                TempData["msg"] = "Vous n'êtes pas autorisé à supprimer ce profil";
                TempData["msgType"] = "alert-danger";
                return RedirectToAction("Index", "Films");
            }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Logout");
        }


        //Users/Login
        public ActionResult Login()
        {
            return View();
        }

        //Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {
            //this action is for handle post (login)
            if (ModelState.IsValid) //this is check validity
            {
                var v = db.Users.Where(a => a.Pseudo.Equals(u.Pseudo) && a.Mdp.Equals(u.Mdp)).FirstOrDefault();
                if (v != null)
                {
                    //string path = Request.ServerVariables["HTTP_REFERRER"]; // Permet de recupere l'url de la page precedente
                    Session["LoginUserID"] = v.Id.ToString();
                    Session["LoginName"] = v.Pseudo.ToString();
                    Session["LoginAdmin"] = v.Admin.ToString();

                    return RedirectToAction("Index","Films"); //Redirection vers la page d'accueil
                }
            }

            ViewBag.msg = "Vos identifiants sont incorrects";
            ViewBag.msgType = "alert-danger";

            return View(u);
        }

        public ActionResult Logout()
        {
            Session.Remove("LoginUserID");
            Session.Remove("LoginName");
            Session.Remove("LoginAdmin");
            Session.RemoveAll();

            return RedirectToAction("Index","Films");
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
