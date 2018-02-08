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

    using Services;

    public class EnrollmentsController : BaseController<Enrollment>
    {
        private IService<Course> courseService;

        public EnrollmentsController(IService<Enrollment> service, IService<User> userService, IService<Course> courseService)
            : base(service, userService)
        {
            this.courseService = courseService;
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Enroll(int courseId)
        {
            string userId = User.Identity.GetUserId();
            var user = userService.GetById(userId);

            if (user.IsTeacher)
            {
                return Unauthorized();
            }

            var course = courseService.GetById(courseId);
            var enrollment = new Enrollment()
            {
                Grade = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CourseId = courseId,
                UserId = userId
            };

            service.Insert(enrollment);

            var enrollmentModel = Mapper.Map<EnrollmentViewModel>(enrollment);

            return Ok(enrollmentModel);
        }

        [HttpPatch]
        [Authorize]
        public IHttpActionResult Grade(int id, double grade)
        {
            string userId = User.Identity.GetUserId();
            var user = userService.GetById(userId);

            if (!user.IsTeacher)
            {
                return Unauthorized();
            }

            var enrollment = service.GetById(id);

            if (enrollment != null)
            {
                if (userId != enrollment.Course.TeacherId)
                {
                    return Unauthorized();
                }

                enrollment.Grade = grade;
                enrollment.UpdatedAt = DateTime.Now;
                service.Update(enrollment);

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
            var user = userService.GetById(userId);

            if (!user.IsTeacher)
            {
                return Unauthorized();
            }

            var enrollments = new List<Enrollment>();

            foreach (var enrollmentGrade in enrollmentGrades)
            {
                var enrollment = service.GetById(enrollmentGrade.Key);

                if (userId == enrollment.Course.TeacherId)
                {
                    enrollment.Grade = enrollmentGrade.Value;
                    enrollment.UpdatedAt = DateTime.Now;
                    service.Update(enrollment);
                }

                enrollments.Add(enrollment);
            }

            var enrollmentModels = Mapper.Map<IEnumerable<EnrollmentViewModel>>(enrollments);

            return Ok(enrollmentModels);
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            string userId = User.Identity.GetUserId();
            var enrollment = service.GetById(id);

            if (enrollment != null)
            {
                if (enrollment.Course.TeacherId != userId && enrollment.UserId != userId)
                {
                    return Unauthorized();
                }

                service.Delete(enrollment);

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}