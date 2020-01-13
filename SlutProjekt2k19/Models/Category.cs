using System.ComponentModel.DataAnnotations;

namespace SlutProjekt2k19.Models
{
    public class Category
    {
        [Key] public int Id { set; get; }
        public string Name { set; get; }
    }
}