using SlutProjekt2k19.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SlutProjekt2k19.Controllers
{
    public class ImagesController : Controller
    {
        // GET: Images
        private DBContext db = new DBContext();

        public ActionResult AddImageView()
        { 
            return View("AddImages");
        }

        public ActionResult AddImage(HttpPostedFileBase file)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var profile = db.profiles.Find(userId);
            var currentImage = profile.Image;

            var profiles = db.profiles.ToList();

            foreach (Profile item in profiles)
            {
                //Har man en bild sen innan tas den gamla bilden bort
                if (currentImage != null)
                {

                    string fullPath = Request.MapPath("~" + currentImage);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                        item.Image = "";
                        db.Entry(item).State = EntityState.Modified;
                    }
                }
                else
                {
                    if (userId == item.Id)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        item.Image = "~/Images/" + fileName;
                        string imgPath = "/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        file.SaveAs(fileName);
                        item.Image = imgPath;
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index", "Profiles");
        }
    }
}

    
