using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using SlutProjekt2k19.Models;

namespace SlutProjekt2k19.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            var profileList = db.Profiles.Take(6).ToList();

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            String userId = null;
            if (claim != null)
            {
                userId = claim.Value;
            }
            

            foreach (Profile profil in profileList)
            {
                if(profil.Id == userId)
                {
                    profileList.Remove(profil);
                    break;
                }
            }

            ViewBag.Files = profileList.ToList();

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