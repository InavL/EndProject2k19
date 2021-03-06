﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;
using SlutProjekt2k19.Models;

namespace SlutProjekt2k19.Controllers
{
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ViewMessage()
        {
            return View();
        }

        // GET: Profiles
        public ActionResult Index()
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity) User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var userId = claim.Value;

                var list = db.Profiles.ToList();
                var profile = new Profile();
                foreach (var item in list.Where(item => userId == item.Id)) profile = item;

                return View(profile);
            }
            catch
            {
                return HttpNotFound();
            }
        }
        [Authorize]
        public ActionResult OtherProfile(string id)
        {
            try
            {
                var list = db.Profiles.ToList();
                var profile = new Profile();
                foreach (var item in list.Where(item => id == item.Id)) profile = item;

                return View(profile);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public ActionResult Feed()
        {
            var usrList = db.Profiles.ToList();


            return View(usrList);
        }

        // GET: Profiles/Details/5
        
        public ActionResult Details(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var profile = db.Profiles.Find(id);
            if (profile == null) return HttpNotFound();

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
        public ActionResult Create([Bind(Include = "Id,Name,Bio,Gender,Age")]
            Profile profile)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity) User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var userId = claim.Value;

                profile.Id = userId;
                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profile);
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var profile = db.Profiles.Find(id);
            if (profile == null) return HttpNotFound();

            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Image,Bio,Gender,SearchingFor,Age")]
            Profile profile)
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
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var profile = db.Profiles.Find(id);
            if (profile == null) return HttpNotFound();

            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var profile = db.Profiles.Find(id);
            db.Profiles.Remove(profile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();

            base.Dispose(disposing);
        }
    }
}