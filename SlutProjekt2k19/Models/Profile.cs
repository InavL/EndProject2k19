using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SlutProjekt2k19.Models
{
    public class Profile
    {
        [Key]
        public int ID { get; set;}
        public string Name { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public string Gender { get; set; }
        public string SearchingFor { get; set; }
        public int Age { get; set; }

        //[ForeignKey("UserCredentials")]
        public int UserCredentials { get; set; }
    }
}