using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SlutProjekt2k19.Models
{
    public class UserCredentialsContext : System.Data.Entity.DbContext
    {
        public DbSet<UserCredentials> userLogins { get; set; }
        public UserCredentialsContext() : base("UserCredentialsDb") { }
    }
}