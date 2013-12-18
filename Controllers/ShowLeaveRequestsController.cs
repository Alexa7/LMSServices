﻿using System;
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

            LeaveRequestsController userleaverequests = new LeaveRequestsController();
            var requests = userleaverequests.GetLeaveRequests();

            return View(requests);
        }

        //
        // GET: /ShowLeaveRequests/UserLeaveRequests

        public ActionResult UserLeaveRequests()
        {

            GetDescriptionController descr = new GetDescriptionController();
            ViewBag.descr = descr;

            LeaveRequestsController userleaverequests = new LeaveRequestsController();
            var requests = userleaverequests.GetUserLeaveRequests(WebSecurity.GetUserId(User.Identity.Name));

            return View(requests);
        }

        //
        // GET: /ShowLeaveRequests/PendingLeaveRequests

        public ActionResult PendingLeaveRequests()
        {

            GetDescriptionController descr = new GetDescriptionController();
            ViewBag.descr = descr;

            LeaveRequestsController userleaverequests = new LeaveRequestsController();
            var requests = userleaverequests.GetLeaveRequest("Q");

            return View(requests);
        }

        //
        // GET: /ShowLeaveRequests/Details/5

        public ActionResult Details(int id = 0)
        {
            GetDescriptionController descr = new GetDescriptionController();
            ViewBag.descr = descr;

            LeaveRequestsController leaverequest = new LeaveRequestsController();
            var request = leaverequest.GetLeaveRequest(id);

            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
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
                leaverequest.UserID = WebSecurity.GetUserId(User.Identity.Name);
                leaverequest.Status = "Q";
                
                CalculateNumOfDaysController c = new CalculateNumOfDaysController();

                leaverequest.NumOfDays = c.Get(leaverequest.FromDate, leaverequest.ToDate);

                LeaveRequestsController request = new LeaveRequestsController();
                var succ = request.PostLeaveRequest(leaverequest);
                
                //db.LeaveRequests.Add(leaverequest);
                //db.SaveChanges();
                return RedirectToAction("UserLeaveRequests");
            }

            return View(leaverequest);
        }

        //
        // GET: /ShowLeaveRequests/Edit/5

        public ActionResult Edit(int id = 0)
        {
            GetDescriptionController descr = new GetDescriptionController();
            ViewBag.descr = descr;

            LeaveRequestsController leaverequest = new LeaveRequestsController();
            var request = leaverequest.GetLeaveRequest(id);

            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        //
        // POST: /ShowLeaveRequests/Edit/5

        [HttpPost]
        public ActionResult Edit(LeaveRequest leaverequest)
        {
            if (ModelState.IsValid)
            {
                LeaveRequestsController request = new LeaveRequestsController();
                var succ = request.PutLeaveRequest(leaverequest.ID, leaverequest );
                if (succ.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //ModelState.AddModelError("puterror", new Exception("Gamithike Ligo.."));
                    return RedirectToAction("UserLeaveRequests");
                }
                else
                {
                    ModelState.AddModelError("puterror", new Exception("Κάτι πήγε στραβά.. Ελέγξτε τα στοιχεία και δοκιμάστε ξανά.."));
                    return View(leaverequest);
                }
                
                //db.Entry(leaverequest).State = EntityState.Modified;
                //db.SaveChanges();


                
            }
            return View(leaverequest);
        }

        //
        // GET: /ShowLeaveRequests/Delete/5

        public ActionResult Delete(int id = 0)
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
        // POST: /ShowLeaveRequests/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaveRequest leaverequest = db.LeaveRequests.Find(id);
            db.LeaveRequests.Remove(leaverequest);
            db.SaveChanges();
            return RedirectToAction("UserLeaveRequests");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}