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
    public class CalcRemainingLeaveDaysController : ApiController
    {
        private DefaultDBContext db = new DefaultDBContext();

        // GET api/CalcRemainingLeaveDays/5
        public int Get(int id, int year = 0)
        {
            if (year < 2004) { year = DateTime.Now.Year; }
         
            LeaveRequestsController userleaverequests = new LeaveRequestsController();

            var acceptedRequests = userleaverequests.GetUserLeaveRequests(id, "A", year);
            int numOfDaysAcquired = 0;

            foreach (var anAcceptedRequest in acceptedRequests)
            {
                numOfDaysAcquired += anAcceptedRequest.AcceptedNumOfDays;
            }

            CalcEligibleLeaveDaysController eligibleleavedays = new CalcEligibleLeaveDaysController();
            int numOfEligibleDays = eligibleleavedays.Get(DateTime.Now);

            var numOfRemainingDays = numOfEligibleDays - numOfDaysAcquired;

            return numOfRemainingDays;
        }
    }
}