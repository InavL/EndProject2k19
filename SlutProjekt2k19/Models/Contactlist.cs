using System.ComponentModel.DataAnnotations;

namespace SlutProjekt2k19.Models
{
    public class Contactlist
    {   
        [Key]
        public int Id { set; get; }
        public int Friend1 { set; get; }
        public int Friend2 { set; get; }
        public string FriendCategory1 { set; get; }
        public string FriendCategory2 { set; get; }
    }
}