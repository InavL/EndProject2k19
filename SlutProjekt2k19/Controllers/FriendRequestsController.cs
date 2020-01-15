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
    public class FriendRequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET: FriendRequests
        [HttpGet]
        public ActionResult Index(string name)
        {
            try
            {
                var profilelist = db.Profiles.ToList();
                var list2 = new List<Profile>();

                foreach (var item in profilelist)
                {
                    var Name = item.Name;
                    if (Name == name)
                    {
                        list2.Add(item);
                    }
                }

                return View(list2);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        // GET: FriendRequests/Details/5
        public ActionResult CountPendingRequests()
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity) this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var userId = claim.Value;

                var friendlist = db.FriendRequests.ToList();
                var profilelist = db.Profiles.ToList();
                var userString = userId.ToString();
                var pendingFriends = new List<string>();
                var cred = 0;
                var friendProfiles = new List<Profile>();

                foreach (var item in profilelist)
                {
                    if (userString == item.Id)
                    {
                        cred = item.UserCredentials;
                    }
                }

                foreach (var item in friendlist)
                {
                    if (cred.ToString() == item.To)
                    {
                        pendingFriends.Add(item.From);
                    }
                }

                foreach (var item in profilelist)
                {
                    foreach (var to in pendingFriends)
                    {
                        if (item.Id == to)
                        {
                            friendProfiles.Add(item);
                        }
                    }
                }

                var count = friendProfiles.Count.ToString();
                return Content(count);
            }
            catch
            {
                return Content(null);
            }
        }

        public ActionResult SendFriendRequest(string id)
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            Console.WriteLine(id);
            db.FriendRequests.ToList();
            var profilelist = db.Profiles.ToList();
            List<Profile> list2;
            list2 = new List<Profile>();
            var userString = userId;
            ViewBag.MyString = "";

            foreach (var item in profilelist)
            {
                if (Convert.ToString(item.UserCredentials) == id && item.Id != userId)
                {
                    var friendrequests = new FriendRequest
                    {
                        From = userString, To = Convert.ToString(item.UserCredentials)
                    };
                    db.FriendRequests.Add(friendrequests);
                    db.SaveChanges();
                    list2.Add(item);
                    ViewBag.MyString = "A new friend request has been sent";
                }
            }

            return RedirectToAction("Feed", "Profiles");
        }

        public ActionResult PendingRequests()
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity) this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var userId = claim.Value;

                var friendlist = db.FriendRequests.ToList();
                var profilelist = db.Profiles.ToList();
                var userString = userId;
                var pendingFriends = new List<string>();
                var cred = 0;
                var friendProfiles = new List<Profile>();

                foreach (var item in profilelist)
                {
                    if (userString == item.Id)
                    {
                        cred = item.UserCredentials;
                    }
                }

                foreach (var item in friendlist)
                {
                    if (cred.ToString() == item.To)
                    {
                        pendingFriends.Add(item.From);
                    }
                }

                foreach (var item in profilelist)
                {
                    foreach (var to in pendingFriends)
                    {
                        if (item.Id == to)
                        {
                            friendProfiles.Add(item);
                        }
                    }
                }

                return View(friendProfiles);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var friendRequest = db.FriendRequests.Find(id);
            if (friendRequest == null)
            {
                return HttpNotFound();
            }

            return View(friendRequest);
        }

        // GET: FriendRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FriendRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,From,To")] FriendRequest friendRequest)
        {
            if (ModelState.IsValid)
            {
                db.FriendRequests.Add(friendRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(friendRequest);
        }

        // GET: FriendRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var friendRequest = db.FriendRequests.ToList();
            return View(friendRequest);
        }

        // POST: FriendRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,From,To")] FriendRequest friendRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(friendRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(friendRequest);
        }

        // GET: FriendRequests/Delete/5
        public ActionResult Delete(string id)
        {
            var claimsIdentity = (ClaimsIdentity) this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var profilelist = db.Profiles.ToList();
            var userString = userId;
            var to = 0;

            foreach (var item in profilelist)
            {
                if (userString == item.Id)
                {
                    to = item.UserCredentials;
                }
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var friendlist = db.FriendRequests.ToList();

            foreach (var item in friendlist)
            {
                if (item.To == to.ToString() && item.From == id.ToString())
                {
                    db.FriendRequests.Remove(item);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("PendingRequests");
        }

        public ActionResult Add(string id)
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var profilelist = db.Profiles.ToList();
            var userString = userId;
            var friendProfiles = new List<Profile>();

            var friendlist = db.FriendRequests.ToList();

            foreach (var itemP in profilelist)
            {
                if (userString == itemP.Id)
                {
                    var to = itemP.UserCredentials;

                    foreach (var itemF in friendlist)
                    {
                        if (itemF.To == to.ToString() && itemF.From == id.ToString())
                        {
                            var contact = new Contactlist();
                            var contact2 = new Contactlist();
                            contact.Friend1 = userString;
                            contact.Friend2 = id;
                            contact2.Friend1 = id;
                            contact2.Friend2 = userString;
                            db.FriendRequests.Remove(itemF);

                            db.Contactlists.Add(contact);
                            db.Contactlists.Add(contact2);
                            db.SaveChanges();

                            foreach (var friend in profilelist)
                            {
                                if (friend.Id == id)
                                {
                                    friendProfiles.Add(friend);
                                }
                            }
                        }
                    }
                }
            }

            return View("../Contactlists/Index", friendProfiles);
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