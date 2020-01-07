using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SlutProjekt2k19.Models
{
    public class DataDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }

        public DataDbContext() : base("projectdb") { }
    }
}