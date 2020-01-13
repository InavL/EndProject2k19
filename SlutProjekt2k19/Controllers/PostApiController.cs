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
            Context();
            var todo = ctx.Posts.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                todo.IsDone = !todo.IsDone;
                ctx.SaveChanges();
            }
        }
    }
}