using SlutProjekt2k19.Models;
using System;
using System.Data.Entity;
using System.Drawing.Printing;
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
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AddImageView()
        {
            return View("AddImages");
        }

        public ActionResult AddImage(HttpPostedFileBase file)
        {
            var claimsIdentity = (ClaimsIdentity) this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var profile = db.Profiles.Find(userId);
            try
            {
                var currentImage = profile.Image;

                if (userId == profile.Id)
                {
                    //Har man en bild sen tidigare tas den gamla bilden bort och ny läggs till
                    if (currentImage != null)
                    {
                        profile.Image = "";
                        db.Entry(profile).State = EntityState.Modified;
                        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        profile.Image = "~/Images/" + fileName;
                        string imgPath = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        file.SaveAs(fileName);
                        profile.Image = imgPath;
                        System.Console.WriteLine(fileName + imgPath);
                        db.Entry(profile).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    //Ny bild läggs till
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    profile.Image = "~/Images/" + fileName;
                    string imgPath = "~/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    file.SaveAs(fileName);
                    profile.Image = imgPath;
                    db.Entry(profile).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                throw;
            }


            return RedirectToAction("Index", "Profiles");
        }
    }
}