using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SlutProjekt2k19.Models;

namespace SlutProjekt2k19.Controllers
{
    public class ProfilesController : Controller
    {
        private DataDbContext db = new DataDbContext();

        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }


        public ProfilesController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        public ActionResult GetProfiles(string search)
        {
            var list = db.Profiles;
            var profiles = from x in list select x;
            if (!string.IsNullOrEmpty(search))
            {
                profiles = list.Where(x => x.Name.Contains(search));
            }
            return View(profiles.ToList());
        }
        public ActionResult SetProfileModel()
        {
            var model = new SlutProjekt2k19.Models.Profile();

            var profiles = db.Profiles;

            
                return View(model);
        }

        // GET: Profiles
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var currentProfile = db.Profiles.FirstOrDefault(p => p.Id == userId);

            return View(db.Profiles.ToList());
        }

        // GET: Profiles/Details/5
        public ActionResult Details()
        {
            var userId = User.Identity.GetUserId();
            var currentProfile = db.Profiles.FirstOrDefault(p => p.Id == userId);

            if (currentProfile == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var profile = db.Profiles.Find(currentProfile);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // GET: Profiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> CreateAsync([Bind(Include = "Name,Age,Gender,Bio")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                var userID = User.Identity.GetUserId();
                var id = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                profile.Id = userID.ToString();

                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profile);
        }


        // GET: Profiles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,Gender,Bio")] Profile profile)
        {

            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Profile profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
