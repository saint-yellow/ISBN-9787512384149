using System.Web.Mvc;
using WindowsOnly.ViewModels;

namespace WindowsOnly.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //[Route("About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Route("Basic")]
        public ActionResult Basic()
        {
            return View();
        }

        [Route("Advanced")]
        public ActionResult Advanced()
        {
            Person person = new Person
            {
                FirstName = "Huang",
                LastName = "Jionghui"
            };

            return View(person);
        }
    }
}