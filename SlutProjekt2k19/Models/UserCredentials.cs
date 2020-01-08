using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SlutProjekt2k19.Models
{
    public class UserCredentials
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Password { get; set; }
    }
}