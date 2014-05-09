using GuessMyNumber.WebClient.Filters;
using System.Web.Mvc;

namespace GuessMyNumber.WebClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Simply select the player you want to challenge and start guessing numbers!";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
