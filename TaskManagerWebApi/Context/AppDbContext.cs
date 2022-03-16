using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        public DbSet<TaskLog> TaskLogTable { get; set; }
      //  public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<FetchUserGroup> FetchUserGroups { get; set; }

        public DbSet<FetchUserGroupTask> FetchUserGroupTasks { get; set; }
        public DbSet<UserGroupTask> UserGroupTasks { get; set; }

        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<GroupTasksByUser> GroupTasks { get; set; }

        public DbSet<AssignedTasks> AssignedTasks { get; set; }

        public DbSet<PendingTasks> PendingTasks { get; set; }


    }
}
