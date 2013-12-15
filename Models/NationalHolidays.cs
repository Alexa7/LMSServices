using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LMSServices.Models
{
    public class NationalHolidays
    {
        public int NationalHolidaysID { get; set; }
        public DateTime HolidayDate { get; set; }
        public string Description { get; set; }
        public bool Movable { get; set; }
    }

    //public class NationalHolidaysDBContext : DbContext
    //{
    //    public NationalHolidaysDBContext() : base("DefaultConnection")
    //    {
    //    }
    //    public DbSet<NationalHolidays> NationalHolidays { get; set; }
    //}
}