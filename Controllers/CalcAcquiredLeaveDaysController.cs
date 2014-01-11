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
    public class CalcAcquiredLeaveDaysController : ApiController
    {
        private DefaultDBContext db = new DefaultDBContext();

        // GET api/CalcAcquiredLeaveDays
        public IEnumerable<LeaveRequest> GetLeaveRequests()
        {
            return db.LeaveRequests.AsEnumerable();
        }

        // GET api/CalcAcquiredLeaveDays/5
        public LeaveRequest Get(int userId)
        {
            LeaveRequest leaverequest = db.LeaveRequests.Find(userId);
            if (leaverequest == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return leaverequest;
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}