using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMSServices.Controllers
{
    public class CalcEligibleLeaveDaysController : ApiController
    {
        // GET api/calceligibleleavedays
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/calceligibleleavedays/5
        public int Get(DateTime registrationDate)
        {
            int numOfDays = 0;
            DateTime today = DateTime.Now;
            if (today.Year > registrationDate.Year)
            {
                numOfDays = 24;
            }
            else
            {
                if (registrationDate.Day < 10)
                {
                    numOfDays += 2;
                }
                else if (registrationDate.Day < 20)
                {
                    numOfDays += 1;
                }

                if (registrationDate.Month != 12)
                {
                    int tempMonth = registrationDate.Month + 1;
                    while (tempMonth <= 12)
                    {
                        numOfDays += 2;
                        tempMonth++;
                    }
                }
            }
            return numOfDays;
        }
    }
}
