using System.ComponentModel.DataAnnotations;

namespace SlutProjekt2k19.Models
{
    public class Posts
    {
        [Key] public int Id { set; get; }
        public string Content { set; get; }
        public int Author { set; get; }
        public int OnProfile { set; get; }
    }
}