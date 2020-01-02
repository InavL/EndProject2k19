using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SlutProjekt2k19.Models;

namespace SlutProjekt2k19.Controllers
{
    public class UserCredentialsController : Controller
    {
        // GET: UserCredentials
        public ActionResult Index()
        {
            var ctx = new UserCredentialsContext();
            var viewModel = new UserCredentialsIndexViewModel
            {
                userLogins = ctx.userLogins.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddUserCredentials(UserCredentials model)
        {
            var ctx = new UserCredentialsContext();
            ctx.userLogins.Add(model);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}