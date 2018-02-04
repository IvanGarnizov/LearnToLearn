namespace LearnToLearn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Entities;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<LearnToLearnContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LearnToLearnContext context)
        {
            if (!context.Users.Any())
            {
                var teacher = new User()
                {
                    UserName = "teacherIvan",
                    Email = "teacher@learn.com",
                    IsTeacher = true
                };
                var firstStudent = new User()
                {
                    UserName = "studentMitko",
                    Email = "student.mitko@learn.com"
                };
                var secondStudent = new User()
                {
                    UserName = "studentSilveto",
                    Email = "student.silveto@learn.com"
                };
                var userManager = new UserManager<User>(new UserStore<User>(context));

                userManager.Create(teacher, "teacherP@ss123");
                userManager.Create(firstStudent, "mitkoP@ss123");
                userManager.Create(secondStudent, "silvetoP@ss123");

                var firstCourse = new Course()
                {
                    Name = "C# Basics",
                    Description = "Learn programming in C#",
                    Capacity = 20,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                var secondCourse = new Course()
                {
                    Name = "Java Basics",
                    Description = "Learn programming in Java",
                    Capacity = 17,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                teacher.Courses.Add(firstCourse);
                teacher.Courses.Add(secondCourse);
                context.SaveChanges();
                firstStudent.Enrollments.Add(new Enrollment()
                {
                    Grade = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CourseId = firstCourse.Id
                });
                firstStudent.Enrollments.Add(new Enrollment()
                {
                    Grade = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CourseId = secondCourse.Id
                });
                secondStudent.Enrollments.Add(new Enrollment()
                {
                    Grade = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CourseId = firstCourse.Id
                });
                secondStudent.Enrollments.Add(new Enrollment()
                {
                    Grade = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CourseId = secondCourse.Id
                });
                context.SaveChanges();
            }
        }
    }
}
