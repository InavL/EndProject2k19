using SlutProjekt2k19.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Routing;
using System.Data.Entity;

namespace SlutProjekt2k19.Controllers
{
    //[RoutePrefix("api/apimessage")]
    public class ApiMessageController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ApiMessageController() 
        {
            var context = new ApplicationDbContext();
        }

        //[Route("list")]
        [HttpGet]
        public IEnumerable<PostMessageDto> List()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            return db.Posts
                .Include(m => m.User)
                .OrderBy(m => m.Timestamp)
                .ToList()
                .Select(m => new PostMessageDto(m));



        }

        //[Route("send")]
        [HttpPost]
        public string Send([FromBody]PostMessageDto messageDto)
        {
            try
            {
                var message = new PostMessage(messageDto);
                db.Posts.Add(message);
                db.SaveChanges();
                return "Ok";
            }
            catch
            {
                return "Inte ok";
            }
        }


    }
}
