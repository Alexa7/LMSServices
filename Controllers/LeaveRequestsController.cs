using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using LMSServices.Models;

namespace LMSServices.Controllers
{
    public class LeaveRequestsController : ApiController
    {
        private DefaultDBContext db = new DefaultDBContext();

        // GET api/LeaveRequests
        public IEnumerable<LeaveRequest> GetLeaveRequests()
        {
            return db.LeaveRequests.AsEnumerable();
        }

        // GET api/LeaveRequests/P
        public IEnumerable<LeaveRequest> GetLeaveRequest(string status)
        {
            string[] leaveStatuses = { "P", "C", "A", "D" };
            if (status == null)
            {
                throw new ArgumentNullException("status");
            }
            else if (!leaveStatuses.Contains(status))
            {
                throw new ArgumentException("The status requested is not valid!");
            }
            
            List<LeaveRequest> leaverequest = db.LeaveRequests.Where(o => o.Status == status).ToList();
            if (leaverequest == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return leaverequest;
        }

        // GET api/LeaveRequests/5
        public LeaveRequest GetLeaveRequest(int id)
        {
            LeaveRequest leaverequest = db.LeaveRequests.Find(id);
            if (leaverequest == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return leaverequest;
        }

        // GET api/LeaveRequests/5
        public IEnumerable<LeaveRequest> GetUserLeaveRequests(int userId, string status=null)
        {
            List<LeaveRequest> leaverequest = null;
            if (status != null)
            {
                leaverequest = db.LeaveRequests.Where(o => o.UserID == userId && o.Status == status).ToList();
            }
            else
            {
                leaverequest = db.LeaveRequests.Where(o => o.UserID == userId).ToList();
            }

            if (leaverequest == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return leaverequest;
        }

        // PUT api/LeaveRequests/5
        public HttpResponseMessage PutLeaveRequest(int id, LeaveRequest leaverequest)
        {
            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
            };

            string errorMessage = ValidateLeaveRequest(leaverequest);

            if (ModelState.IsValid && id == leaverequest.ID && string.IsNullOrEmpty(errorMessage))
            {
                db.Entry(leaverequest).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                    response.StatusCode = HttpStatusCode.OK;
                }
                catch (DbUpdateConcurrencyException)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Content = new StringContent("Η εγγραφή δεν βρέθηκε");
                }
            }
            else
            {
                response.Content = new StringContent(errorMessage);
            }

            return response;
        }

        // POST api/LeaveRequests
        public HttpResponseMessage PostLeaveRequest(LeaveRequest leaverequest)
        {
            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
            };

            string errorMessage = ValidateLeaveRequest(leaverequest);

            if (ModelState.IsValid && string.IsNullOrEmpty(errorMessage))
            {
                db.LeaveRequests.Add(leaverequest);
                db.SaveChanges();

                response.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.Content = new StringContent(errorMessage);
            }

            return response;
        }

        // DELETE api/LeaveRequests/5
        public HttpResponseMessage DeleteLeaveRequest(int id)
        {
            LeaveRequest leaverequest = db.LeaveRequests.Find(id);
            if (leaverequest == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.LeaveRequests.Remove(leaverequest);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, leaverequest);
        }

        /// <summary>
        /// Performs leave request validation
        /// </summary>
        /// <param name="leaveRequest">The leave request to validate</param>
        /// <returns>The error message, empty string if request is valid</returns>
        protected string ValidateLeaveRequest(LeaveRequest leaveRequest)
        {
            if (leaveRequest.FromDate > leaveRequest.ToDate)
            {
                return "Η ημερομηνία έναρξης άδειας δεν μπορεί να είναι μεγαλύτερη από την ημερομηνία λήξης";
            }
            if (leaveRequest.FromDate < DateTime.Now)
            {
                return "Η ημερομηνία έξαρξης άδειας έχει παρέλθει";
            }
            if (leaveRequest.NumOfDays < 0)
            {
                return string.Format("Δεν μπορείτε να πάρετε περισσότερες από {0} μέρες άδειας", 0);
            }
            return string.Empty;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}