using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SlutProjekt2k19.Models
{
    public class Posts
    {
        [Key]
        public int ID { set; get; }
        public string Content { set; get; }
        public int Author { set; get; }

        public int OnProfile { set; get; }
    }
}