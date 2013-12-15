using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSServices.Models;

namespace LMSServices.Controllers
{
    public class UsersController : Controller
    {
        private UserDBContext db = new UserDBContext();

        //
        // GET: /Users/

        public ActionResult Index()
        {
            return View(db.AppUser.ToList());
        }

        //
        // GET: /Users/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.AppUser.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.AppUser.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /Users/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.AppUser.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Users/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.AppUser.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.AppUser.Find(id);
            db.AppUser.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}