using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMSServices.Controllers
{
    public class GetDescriptionController : ApiController
    {
        // GET api/getdescription
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/getdescription/5
        public string Get(string type, string value )
        {
            string description = "";

            if (type == "LeaveRequestStatus")
            {
                if (value == "P")
                {
                    description = "Εκκρεμεί";
                }
                else if (value == "A")
                {
                    description = "Αποδεκτή";
                }
                else if (value == "D")
                {
                    description = "Απερρίφθει";
                }
                else if (value == "C")
                {
                    description = "Ακυρωμένη";
                }
                else
                {
                    return value;
                }
            }
            else if (type == "LeaveRequestType")
            {
                if (value == "L")
                {
                    description = "Άδεια Διακοπών";
                }
                else if (value == "Α")
                {
                    description = "Άδεια Ασθενείας";
                }
                else if (value == "U")
                {
                    description = "Άδεια άνευ Αποδοχών";
                }
                else
                {
                    return value;
                }
            }
            else if (type == "LeaveRequestColumns")
            {
                if (value == "UserID")
                {
                    description = "Χρήστης";
                }
                else if (value == "FromDate")
                {
                    description = "Από";
                }
                else if (value == "ToDate")
                {
                    description = "Έως";
                }
                else if (value == "AcceptedFromDate")
                {
                    description = "Αποδεκτή από";
                }
                else if (value == "AcceptedToDate")
                {
                    description = "Αποδεκτή έως";
                }
                else if (value == "Type")
                {
                    description = "Τύπος Άδειας";
                }
                else if (value == "NumOfDays")
                {
                    description = "Αριθμός Ημερών";
                }
                else if (value == "Status")
                {
                    description = "Κατάσταση";
                }
                else
                {
                    return value;
                }
            }
            else if (type == "UserType") 
            {
                if (value == "HumanResources")
                {
                    description = "Human Resources";
                }
                else if (value == "ProjectManager")
                {
                    description = "Project Manager";
                }
                else if (value == "SoftwareEngineer")
                {
                    description = "Software Engineer";
                }
                else if (value == "OfficeStaff")
                {
                    description = "Office Staff";
                }
                else
                {
                    return value;
                }
            }

            else
            {
                return "value";
            }

            return description;
        }

    }
}
