using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlutProjekt2k19.Models
{
    public class Profile
    {
        [Key] public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public int UserCredentials { get; set; }
    }
}