using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSServices.Models;
using WebMatrix.WebData;

namespace LMSServices.Controllers
{
    public class ShowLeaveRequestsController : Controller
    {
        private DefaultDBContext db = new DefaultDBContext();

        //
        // GET: /ShowLeaveRequests/

        public ActionResult Index()
        {
            GetDescriptionController descr = new GetDescriptionController();
            ViewBag.descr = descr;
            return View(db.LeaveRequests.ToList());
        }

        //
        // GET: /ShowLeaveRequests/UserLeaveRequests

        public ActionResult UserLeaveRequests()
        {

            GetDescriptionController descr = new GetDescriptionController();
            ViewBag.descr = descr;

            LeaveRequestsController userleaverequests = new LeaveRequestsController();
            var requests = userleaverequests.GetUserLeaveRequests(WebSecurity.GetUserId(User.Identity.Name));
            //ViewBag.userleaverequests = userleaverequests;

            return View(requests);
        }

        //
        // GET: /ShowLeaveRequests/Details/5

        public ActionResult Details(int id = 0)
        {
            GetDescriptionController descr = new GetDescriptionController();
            ViewBag.descr = descr;
            LeaveRequest leaverequest = db.LeaveRequests.Find(id);
            if (leaverequest == null)
            {
                return HttpNotFound();
            }
            return View(leaverequest);
        }

        //
        // GET: /ShowLeaveRequests/Create

        public ActionResult Create()
        {
           GetDescriptionController descr = new GetDescriptionController();
           ViewBag.descr = descr;
            return View();
        }

        //
        // POST: /ShowLeaveRequests/Create

        [HttpPost]
        public ActionResult Create(LeaveRequest leaverequest)
        {
            //this.User
            if (ModelState.IsValid)
            {
                leaverequest.Status = "Q";
                
                CalculateNumOfDaysController c = new CalculateNumOfDaysController();

                leaverequest.NumOfDays = c.Get(leaverequest.FromDate, leaverequest.ToDate);
                
                db.LeaveRequests.Add(leaverequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leaverequest);
        }

        //
        // GET: /ShowLeaveRequests/Edit/5

        public ActionResult Edit(int id = 0)
        {
            GetDescriptionController descr = new GetDescriptionController();
            ViewBag.descr = descr;
            LeaveRequest leaverequest = db.LeaveRequests.Find(id);
            if (leaverequest == null)
            {
                return HttpNotFound();
            }
            return View(leaverequest);
        }

        //
        // POST: /ShowLeaveRequests/Edit/5

        [HttpPost]
        public ActionResult Edit(LeaveRequest leaverequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaverequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leaverequest);
        }

        //
        // GET: /ShowLeaveRequests/Delete/5

        public ActionResult Delete(int id = 0)
        {
            LeaveRequest leaverequest = db.LeaveRequests.Find(id);
            if (leaverequest == null)
            {
                return HttpNotFound();
            }
            return View(leaverequest);
        }

        //
        // POST: /ShowLeaveRequests/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaveRequest leaverequest = db.LeaveRequests.Find(id);
            db.LeaveRequests.Remove(leaverequest);
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