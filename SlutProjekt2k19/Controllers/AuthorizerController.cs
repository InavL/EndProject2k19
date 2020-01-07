using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SlutProjekt2k19.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SlutProjekt2k19.Controllers
{
    public class AuthorizerController : Controller
    {

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyUser(Profile profile)
        {
            var context = new DataDbContext();

            var userId = User.Identity.GetUserId();
            var currentProfile = context.Profiles.FirstOrDefault(p => p.Id == userId);

            if (currentProfile.Id.Equals(profile.Id))
                {
                    
                }
            return View();
           
        }
    }
}