using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;
using SlutProjekt2k19.Models;

namespace SlutProjekt2k19.Controllers
{
    public class ContactlistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contactlists
        public ActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var contacts = db.Contactlists.ToList();
            var profiles = db.Profiles.ToList();
            var contactList = new List<Profile>();
            foreach (var itemC in contacts)
                if (userId == Convert.ToString(itemC.Friend1))
                    foreach (var itemP in profiles)
                        if (Convert.ToString(itemC.Friend2) == itemP.Id)
                            contactList.Add(itemP);

            return View(contactList);
        }

        // GET: Contactlists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var contactlist = db.Contactlists.Find(id);
            if (contactlist == null) return HttpNotFound();
            return View(contactlist);
        }

        // GET: Contactlists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contactlists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Friend1,Friend2,FriendCategory2")]
            Contactlist contactlist)
        {
            if (ModelState.IsValid)
            {
                db.Contactlists.Add(contactlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactlist);
        }

        // GET: Contactlists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var contactlist = db.Contactlists.Find(id);
            if (contactlist == null) return HttpNotFound();
            return View(contactlist);
        }

        // POST: Contactlists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Friend1,Friend2,FriendCategory2")]
            Contactlist contactlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactlist);
        }

        // GET: Contactlists/Delete/5
        public ActionResult Delete(string id)
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var contactlist = db.Contactlists.ToList();

            var userString = userId.ToString();

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            foreach (var item in contactlist)
                if (userString == item.Friend1 && id == item.Friend2)
                {
                    db.Contactlists.Remove(item);
                    db.SaveChanges();
                }

            return RedirectToAction("Index");
        }

        // POST: Contactlists/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var contactlist = db.Contactlists.Find(id);
            db.Contactlists.Remove(contactlist);
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