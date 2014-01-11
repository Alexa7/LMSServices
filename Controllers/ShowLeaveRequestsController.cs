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

            var acceptedRequests = userleaverequests.GetUserLeaveRequests(WebSecurity.GetUserId(User.Identity.Name), "A");
            int numOfDaysAcquired = 0;

            foreach (var anAcceptedRequest in acceptedRequests)
            {
                numOfDaysAcquired += anAcceptedRequest.AcceptedNumOfDays;
            }
            ViewBag.numOfDaysAcquired = numOfDaysAcquired;

            CalcEligibleLeaveDaysController eligibleleavedays = new CalcEligibleLeaveDaysController();
            int numOfEligibleDays = eligibleleavedays.Get(DateTime.Now);
            ViewBag.numOfEligibleDays = numOfEligibleDays;

            return View(requests);
        }

        //
        // GET: /ShowLeaveRequests/UserLeaveRequests

        public ActionResult UserAcceptedRequests()
        {

            GetDescriptionController descr = new GetDescriptionController();
            ViewBag.descr = descr;

            LeaveRequestsController userleaverequests = new LeaveRequestsController();
            var requests = userleaverequests.GetUserLeaveRequests(WebSecurity.GetUserId(User.Identity.Name), "A");

            return View(requests);
        }

        //
        // GET: /ShowLeaveRequests/PendingLeaveRequests

        public ActionResult PendingLeaveRequests()
        {

            GetDescriptionController descr = new GetDescriptionController();
            ViewBag.descr = descr;

            LeaveRequestsController userleaverequests = new LeaveRequestsController();
            var requests = userleaverequests.GetLeaveRequest("P");

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
                leaverequest.Status = "P";
                
                CalculateNumOfDaysController c = new CalculateNumOfDaysController();
                leaverequest.NumOfDays = c.Get(leaverequest.FromDate, leaverequest.ToDate);

                LeaveRequestsController request = new LeaveRequestsController();
                var response = request.PostLeaveRequest(leaverequest);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("UserLeaveRequests");
                }
                else
                {
                    ModelState.AddModelError("createError", new Exception(response.Content.ReadAsStringAsync().Result));
                }
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

                if (leaverequest.AcceptedFromDate.HasValue && leaverequest.AcceptedToDate.HasValue) 
                {
                    CalculateNumOfDaysController c = new CalculateNumOfDaysController();
                    leaverequest.AcceptedNumOfDays = c.Get(leaverequest.AcceptedFromDate.Value, leaverequest.AcceptedToDate.Value);
                    leaverequest.Status = "A";
                    leaverequest.AcceptedBy = WebSecurity.GetUserId(User.Identity.Name);
                }


                var succ = request.PutLeaveRequest(leaverequest.ID, leaverequest );
                if (succ.StatusCode == System.Net.HttpStatusCode.OK)
                {   
                    if (User.IsInRole("HumanResources")) 
                    {
                        return RedirectToAction("PendingLeaveRequests");
                    }
                    else 
                    {
                        return RedirectToAction("UserLeaveRequests");
                    }
                }
                else
                {
                    ModelState.AddModelError("putError", succ.Content.ReadAsStringAsync().Result);
                    //ModelState.AddModelError("putError", new Exception("Κάτι πήγε στραβά.. Ελέγξτε τα στοιχεία και δοκιμάστε ξανά.."));
                }                
            }

            GetDescriptionController descr = new GetDescriptionController();
            ViewBag.descr = descr;
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