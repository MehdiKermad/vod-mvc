using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
                return View(db.Users.ToList());
            }
            else
            {
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

            //ViewBag.msg = "Vous n'avez pas le droit de voir ce profil";
            //ViewBag.msgType = "warning";
            
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
            if ((String)Session["LoginAdmin"] == "True")
                return View(user);
            else
                return RedirectToAction("Index", "Films");
        }

        // POST: Users/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Pseudo,Mdp,Admin,FirstName,LastName,Birth")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Films");
            }
            if ((String)Session["LoginAdmin"] == "True")
                return View(user);
            else
                return RedirectToAction("Index", "Films");
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
            if ((String)Session["LoginAdmin"] == "True")
                return View(user);
            else
                return RedirectToAction("Index", "Films");
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index", "Films");
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
            return View(u);
        }

        public ActionResult Logout()
        {
            Session.Remove("LoginUserID");
            Session.Remove("LoginName");
            Session.Remove("LoginAdmin");
            Session.RemoveAll();

            return Redirect("~/Films");
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
