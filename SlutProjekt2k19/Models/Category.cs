﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SlutProjekt2k19.Models
{
    public class Category
    {
        [Key]
        public int ID { set; get; }

        public string Name  { set; get; }
    }
}