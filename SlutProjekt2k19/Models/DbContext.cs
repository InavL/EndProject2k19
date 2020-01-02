using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SlutProjekt2k19.Models
{
    public class DbContext: System.Data.Entity.DbContext
    {
        public DbSet<Profile> profiles { get; set; }

        public DbContext() : base("DbContext"){}
    }
}