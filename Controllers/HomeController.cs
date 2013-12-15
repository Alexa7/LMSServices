using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSServices.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message1 = "Σας καλωσορίζουμε στην διαδικτυακή εφαρμογή διαχείρισης αδειών της εταιρείας ΑΒΓ Πληροφοριακά Συστήματα Ε.Π.Ε.";
            ViewBag.Message2 = "Η υπηρεσία μας σας παρέχει τη δυνατότητα να δείτε όλες τις τρέχουσες αιτήσεις σας, τον αριθμό αδειών που απομένουν, να πραγματοποιήσετε νέα αίτηση άδειας κ.α..";
            ViewBag.Message3 = "Για να εισέλθετε στην υπηρεσία μας, παρακαλούμε ακολουθήστε τον παρακάτω σύνδεσμο, ώστε να πραγματοποιήσετε σύνδεση, χρησιμοποιώντας τα στοιχεία σας. Σε περίπτωση που δεν έχετε ακόμη κάποιο λογαριασμό, παρακαλούμε όπως επικοινωνήσετε με τον υπεύθυνο Ανθρώπινου Δυναμικού της εταιρείας μας.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Σε περίπτωση που έχετε κάποιο πρόβλημα, παρακαλούμε χρησιμοποιήστε κάποιο από τα ακόλουθα στοιχεία επικοινωνίας.";

            return View();
        }
    }
}
