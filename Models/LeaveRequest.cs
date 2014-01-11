using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LMSServices.Models
{
    public class LeaveRequest
    {
        public int ID {get; set;}
        public int UserID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int NumOfDays { get; set; }
        public Nullable<DateTime> AcceptedFromDate { get; set; }
        public Nullable<DateTime> AcceptedToDate { get; set; }
        public int AcceptedNumOfDays { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int? AcceptedBy { get; set; }
    }

    public class DefaultDBContext : DbContext
    {
        public DefaultDBContext() : base("DefaultConnection") 
        { 
        }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<NationalHolidays> NationalHolidays { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}