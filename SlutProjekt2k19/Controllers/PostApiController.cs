using System.Linq;
using System.Web.Http;
using SlutProjekt2k19.Models;

namespace SlutProjekt2k19.Controllers
{
    // Ändra grundsökvägen så att alla metoder i kontrollern
    // hamnar under /api/todos
    [RoutePrefix("api/posts")]
    public class TodoApiController : ApiController
    {
        /*

        [HttpGet] // Anropa med $.get
        [Route("")] // api/posts
            
        public Posts[] GetAll()
        {
            return new DBContext().Posts.OrderBy(t => t.IsDone ? 1 : 0).ToArray();
        }

        [HttpGet]
        [Route("toggle")]
        public void Toggle(int id)
        {
            // /api/posts/toggle?id=XXX
            var ctx = new DBContext();
            var post = ctx.Posts.FirstOrDefault(t => t.Id == id);
            if (post != null)
            {
                post.IsDone = !post.IsDone;
                ctx.SaveChanges();
            }
        }
        */
    }
}