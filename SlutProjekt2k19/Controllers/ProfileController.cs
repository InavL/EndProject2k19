using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlutProjekt2k19.Models;

namespace SlutProjekt2k19.Models
{
    public class ProfileController : Controller
    {

        
        public ActionResult ProfileIndex()
        {

           
            return View();
        }

        public ActionResult OpenProfileIndex()
        {
            return View("ProfileIndex");
        }

        [HttpPost]
        public ActionResult AddProfile(Profile model)
        {
            var ctx = new DbContext();
            ctx.profiles.Add(model);
            ctx.SaveChanges();

            return RedirectToAction("ProfileIndex");

            
        }
    }
}