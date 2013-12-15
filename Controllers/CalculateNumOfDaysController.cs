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
    public class CalculateNumOfDaysController : ApiController
    {
        private DefaultDBContext db = new DefaultDBContext();

        // GET api/CalculateNumOfDays
        public DateTime Get()
        {
            return DateTime.Now;
            //return Request.CreateResponse<object>(HttpStatusCode.OK, new { FromDate = DateTime.Now, ToDate = DateTime.Now.AddDays(30) });
        }

        // GET api/CalculateNumOfDays/5
        public int Get(DateTime fromDate, DateTime toDate)
        {
            if (fromDate == null || toDate == null)
            {
                throw new ArgumentNullException("The method received a null parameter");
            }

            NationalHolidaysController holidays = new NationalHolidaysController();

            List<NationalHolidays> nationalholidays = holidays.GetNationalHolidays(fromDate, toDate ); //db.NationalHolidays.Where(o => o.HolidayDate >= fromDate && o.HolidayDate <= toDate).ToList();
            if (nationalholidays == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            int NumOfDays = 0;
            int NumOfHolidays = 0;
            DateTime tempDate = fromDate;

            while (tempDate <= toDate)
            {
                if (tempDate.DayOfWeek != DayOfWeek.Saturday && tempDate.DayOfWeek != DayOfWeek.Sunday) { NumOfDays++; }
                tempDate = tempDate.AddDays(1);
            }

            foreach (var aholiday in nationalholidays)
            {
                if (aholiday.HolidayDate.DayOfWeek != DayOfWeek.Saturday && aholiday.HolidayDate.DayOfWeek != DayOfWeek.Sunday) { NumOfHolidays++; }
            }

            NumOfDays -= NumOfHolidays;

            return NumOfDays;
        }
    }
}