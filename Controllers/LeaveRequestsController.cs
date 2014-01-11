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
            var status = HttpStatusCode.OK;
 
            if (ModelState.IsValid && id == leaverequest.ID)
            {
                db.Entry(leaverequest).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    status = HttpStatusCode.NotFound;
                }

                status = HttpStatusCode.OK;
            }
            else
            {
               status = HttpStatusCode.BadRequest;
            }

            if (Request == null)
            {
                return new HttpResponseMessage(status);
            }
            else
            {
                return Request.CreateResponse(status);
            }
        }

        // POST api/LeaveRequests
        public HttpResponseMessage PostLeaveRequest(LeaveRequest leaverequest)
        {
            var status = HttpStatusCode.OK;

            if (ModelState.IsValid)
            {
                db.LeaveRequests.Add(leaverequest);
                db.SaveChanges();

                status = HttpStatusCode.Created;
                                
                //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, leaverequest);
                //response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = leaverequest.ID }));
                //return response;
            }
            else
            {
                status = HttpStatusCode.BadRequest;
            }

            if (Request == null)
            {
                return new HttpResponseMessage(status);
            }
            else
            {
                return Request.CreateResponse(status);
            }
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}