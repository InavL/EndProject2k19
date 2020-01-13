using System.Data.Entity;

namespace SlutProjekt2k19.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Posts> Posts { get; set; }
        
        public DBContext() : base("slutprojectdb"){}
    }
}