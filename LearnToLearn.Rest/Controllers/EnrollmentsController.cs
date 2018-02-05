namespace LearnToLearn.Rest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using AutoMapper;

    using Entities;

    using Microsoft.AspNet.Identity;

    using Models;

    public class EnrollmentsController : BaseController
    {
        [HttpPost]
        [Authorize]
        public IHttpActionResult Enroll(int courseId)
        {
            string userId = User.Identity.GetUserId();
            var user = context.Users
                .FirstOrDefault(u => u.Id == userId);

            if (user.IsTeacher)
            {
                return Unauthorized();
            }

            var course = context.Courses
                .FirstOrDefault(c => c.Id == courseId);
            var enrollment = new Enrollment()
            {
                Grade = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Course = course,
                User = user
            };

            context.Enrollments.Add(enrollment);
            context.SaveChanges();

            var enrollmentModel = Mapper.Map<EnrollmentViewModel>(enrollment);

            return Ok(enrollmentModel);
        }

        [HttpPatch]
        [Authorize]
        public IHttpActionResult Grade(int id, double grade)
        {
            string userId = User.Identity.GetUserId();
            var user = context.Users
                .FirstOrDefault(u => u.Id == userId);

            if (!user.IsTeacher)
            {
                return Unauthorized();
            }

            var enrollment = context.Enrollments
                .FirstOrDefault(e => e.Id == id);

            if (enrollment != null)
            {
                if (userId != enrollment.Course.TeacherId)
                {
                    return Unauthorized();
                }

                enrollment.Grade = grade;
                enrollment.UpdatedAt = DateTime.Now;
                context.SaveChanges();

                var enrollmentModel = Mapper.Map<EnrollmentViewModel>(enrollment);

                return Ok(enrollmentModel);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/enrollments/gradeMany")]
        [HttpPatch]
        [Authorize]
        public IHttpActionResult GradeMany([FromUri]IDictionary<int, double> enrollmentGrades)
        {
            string userId = User.Identity.GetUserId();
            var user = context.Users
                .FirstOrDefault(u => u.Id == userId);

            if (!user.IsTeacher)
            {
                return Unauthorized();
            }

            var enrollments = new List<Enrollment>();

            foreach (var enrollmentGrade in enrollmentGrades)
            {
                var enrollment = context.Enrollments
                    .FirstOrDefault(e => e.Id == enrollmentGrade.Key);

                if (userId == enrollment.Course.TeacherId)
                {
                    enrollment.Grade = enrollmentGrade.Value;
                    enrollment.UpdatedAt = DateTime.Now;
                }

                enrollments.Add(enrollment);
                context.SaveChanges();
            }

            var enrollmentModels = Mapper.Map<IEnumerable<EnrollmentViewModel>>(enrollments);

            return Ok(enrollmentModels);
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            string userId = User.Identity.GetUserId();
            var enrollment = context.Enrollments
                .FirstOrDefault(e => e.Id == id);

            if (enrollment != null)
            {
                if (enrollment.Course.TeacherId != userId && enrollment.UserId != userId)
                {
                    return Unauthorized();
                }

                context.Enrollments.Remove(enrollment);
                context.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}