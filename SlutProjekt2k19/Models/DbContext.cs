using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SlutProjekt2k19.Models
{
    public class DBContext: System.Data.Entity.DbContext
    {
        public DbSet<Profile> profiles { get; set; }
        public DbSet<UserCredentials> userLogins { get; set; }

        public System.Data.Entity.DbSet<SlutProjekt2k19.Models.Posts> Posts { get; set; }
        public DBContext() : base("TheDBContext"){}

        
    }
}