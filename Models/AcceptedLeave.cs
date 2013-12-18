using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSServices.Models
{
    public class AcceptedLeave
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Type { get; set; }
        public int NumOfDays { get; set; }
        public string AcceptedBy { get; set; }
    }
}