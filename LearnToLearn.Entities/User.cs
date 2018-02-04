namespace LearnToLearn.Entities
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    
    public class User : IdentityUser
    {
        private ICollection<Course> courses;
        private ICollection<Enrollment> enrollments;

        public User()
        {
            courses = new HashSet<Course>();
            enrollments = new HashSet<Enrollment>();
        }

        public bool IsTeacher { get; set; }

        public virtual ICollection<Course> Courses
        {
            get
            {
                return courses;
            }

            set
            {
                courses = value;
            }
        }

        public virtual ICollection<Enrollment> Enrollments
        {
            get
            {
                return enrollments;
            }

            set
            {
                enrollments = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            return userIdentity;
        }
    }
}
