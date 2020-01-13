using System.Data.Entity;

namespace SlutProjekt2k19.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }

        public DbSet<PostMessage> Posts { get; set; }
        
        public DbSet<FriendRequest> FriendRequests { get; set; }

        public DbSet<Contactlist> Contactlists { get; set; }

        public DBContext() : base("slutprojectdb"){}
    }
}