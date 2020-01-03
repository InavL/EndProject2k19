using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace SlutProjekt2k19.Models
{
    public class Contactlist
    {   
        [Key]
        public int ID { set; get; }
        public int Friend1 { set; get; }
        public int Friend2 { set; get; }
        public string FriendCategory1 { set; get; }
        public string FriendCategory2 { set; get; }
    }
}