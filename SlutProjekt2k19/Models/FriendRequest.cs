using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SlutProjekt2k19.Models
{
    public class FriendRequest
    {
        public int ID { set; get; }
        public int From { set; get; }
        public int To { set; get; }
    }
}