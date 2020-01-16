using SlutProjekt2k19.Models;
using System;
using System.Data.Entity;
using System.IO;
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
            var claimsIdentity = (ClaimsIdentity) User.Identity;
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
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var extension = Path.GetExtension(file.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        profile.Image = "~/Images/" + fileName;
                        var imgPath = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        file.SaveAs(fileName);
                        profile.Image = imgPath;
                        Console.WriteLine(fileName + imgPath);
                        db.Entry(profile).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    //Ny bild läggs till
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    profile.Image = "~/Images/" + fileName;
                    var imgPath = "~/Images/" + fileName;
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