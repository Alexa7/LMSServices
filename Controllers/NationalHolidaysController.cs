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
    public class NationalHolidaysController : ApiController
    {
        private DefaultDBContext db = new DefaultDBContext();

        // GET api/NationalHolidays
        public IEnumerable<NationalHolidays> GetNationalHolidays()
        {
            return db.NationalHolidays.AsEnumerable();
        }

        // GET api/NationalHolidays/5
        public IEnumerable<NationalHolidays> GetNationalHolidays(int year)
        {
            DateTime minDate = new DateTime(year, 1, 1);
            DateTime maxDate = new DateTime(year, 12, 31);

            List<NationalHolidays> nationalholidays = db.NationalHolidays.Where(o => o.HolidayDate >= minDate && o.HolidayDate <= maxDate).ToList();
            if (nationalholidays == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return nationalholidays;
        }

        // GET api/NationalHolidays/5/5
        public List<NationalHolidays> GetNationalHolidays(DateTime fromDate, DateTime toDate)
        {

            List<NationalHolidays> nationalholidays = db.NationalHolidays.Where(o => o.HolidayDate >= fromDate && o.HolidayDate <= toDate).ToList();
            if (nationalholidays == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return nationalholidays;
        }

        //TODO:
        // PUT api/NationalHolidays/5
        public HttpResponseMessage PutNationalHolidays(int id, NationalHolidays nationalholidays)
        {
            if (ModelState.IsValid && id == nationalholidays.NationalHolidaysID)
            {
                db.Entry(nationalholidays).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        //TODO:
        // POST api/NationalHolidays
        public HttpResponseMessage PostNationalHolidays(NationalHolidays nationalholidays)
        {
            if (ModelState.IsValid)
            {
                db.NationalHolidays.Add(nationalholidays);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, nationalholidays);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = nationalholidays.NationalHolidaysID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        //TODO:
        // DELETE api/NationalHolidays/5
        public HttpResponseMessage DeleteNationalHolidays(int id)
        {
            NationalHolidays nationalholidays = db.NationalHolidays.Find(id);
            if (nationalholidays == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.NationalHolidays.Remove(nationalholidays);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, nationalholidays);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}