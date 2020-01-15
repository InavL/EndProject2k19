using System.Linq;
using System.Web.Mvc;
using SlutProjekt2k19.Models;

namespace SlutProjekt2k19.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            var profileList = db.Profiles.ToList();

            var imageList = profileList.Select(profile => profile.Image).ToList();

            ViewBag.Files = imageList;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}