using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenDiscussion.Models;

namespace OpenDiscussion.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet <Comment> Comments { get; set; }
        public DbSet <Category> Categories { get; set; }
        public DbSet <Discussion> Discussions { get; set; }
        public DbSet <Topic> Topics { get; set; }
    }
}