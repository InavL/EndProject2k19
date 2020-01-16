using SlutProjekt2k19.Models;
using System.Web.Http;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Message = SlutProjekt2k19.Models.Message;

namespace SlutProjekt2k19.Controllers
{
    [RoutePrefix("api/messages")]
    public class MessageApiController : ApiController
    {
        public struct MessageRequestBody
        {
            public string message;
            public string currentUserId;
            public string author;
            public string to;
        }

        [Route("send")]
        public async Task<IHttpActionResult> PostMessage()
        {
            var body = await Request.Content.ReadAsStringAsync();
            var requestMessageBody = JsonConvert.DeserializeObject<MessageRequestBody>(body);

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var db = new ApplicationDbContext();
            var list = db.Profiles.ToList();
            var profile = new Profile();

            foreach (var p in list.Where(p => p.Id.Equals(userId)))
            {
                profile = p;
            }

            var ctx = new MessageDbContext();
            var newMessage = new Message
            {
                Id = Guid.NewGuid().ToString(),
                MessageSent = requestMessageBody.message,
                From = requestMessageBody.currentUserId,
                Author = requestMessageBody.author,
                To = requestMessageBody.to,
                MessagePic = profile.Image
            };

            ctx.Messages.Add(newMessage);
            ctx.SaveChanges();

            return Json(newMessage);
        }
    }
}