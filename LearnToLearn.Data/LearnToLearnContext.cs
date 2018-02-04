namespace LearnToLearn.Data
{
    using System.Data.Entity;

    using Entities;

    using Migrations;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class LearnToLearnContext : IdentityDbContext<User>
    {
        public LearnToLearnContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LearnToLearnContext, Configuration>());
        }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Enrollment> Enrollments { get; set; }

        public static LearnToLearnContext Create()
        {
            return new LearnToLearnContext();
        }
    }
}