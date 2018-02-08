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
    
    public class CoursesController : BaseController<Course>
    {
        public CoursesController(IService<Course> service, IService<User> userService)
            : base(service, userService)
        {
        }

        public IHttpActionResult Get()
        {
            var courses = service.Get()
                .Where(c => c.IsVisible)
                .OrderByDescending(c => c.CreatedAt);

            if (courses.Count() > 0)
            {
                var courseModels = Mapper.Map<IEnumerable<CourseViewModel>>(courses);

                return Ok(courseModels);
            }
            else
            {
                return Ok("There are no courses.");
            }
        }

        public IHttpActionResult Get(int id)
        {
            var course = service.GetById(id);

            if (course != null)
            {
                var courseModel = Mapper.Map<CourseViewModel>(course);

                return Ok(courseModel);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        public IHttpActionResult Post(CourseBindingModel model)
        {
            string userId = User.Identity.GetUserId();
            var user = userService.GetById(userId);

            if (!user.IsTeacher)
            {
                return Unauthorized();
            }

            var course = new Course()
            {
                Name = model.Name,
                Description = model.Description,
                Capacity = model.Capacity,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                TeacherId = userId
            };

            service.Insert(course);
            
            var courseModel = Mapper.Map<CourseViewModel>(course);

            return CreatedAtRoute("DefaultApi", new { id = course.Id }, courseModel);
        }

        [Authorize]
        public IHttpActionResult Put(CourseBindingModel model, int id)
        {
            string userId = User.Identity.GetUserId();
            var user = userService.GetById(userId);

            if (!user.IsTeacher)
            {
                return Unauthorized();
            }

            var course = service.GetById(id);

            if (course != null)
            {
                if (course.TeacherId != userId)
                {
                    return Unauthorized();
                }

                course.Name = model.Name;
                course.Description = model.Description;
                course.Capacity = model.Capacity;
                course.UpdatedAt = DateTime.Now;
                service.Update(course);

                var courseModel = Mapper.Map<CourseViewModel>(course);

                return Ok(courseModel);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            string userId = User.Identity.GetUserId();
            var user = userService.GetById(userId);

            if (!user.IsTeacher)
            {
                return Unauthorized();
            }

            var course = service.GetById(id);

            if (course != null)
            {
                if (course.TeacherId != userId)
                {
                    return Unauthorized();
                }

                service.Delete(course);

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPatch]
        [Authorize]
        public IHttpActionResult ToggleVisibility(int id)
        {
            string userId = User.Identity.GetUserId();
            var user = userService.GetById(userId);

            if (!user.IsTeacher)
            {
                return Unauthorized();
            }

            var course = service.GetById(id);

            if (course != null)
            {
                if (course.TeacherId != userId)
                {
                    return Unauthorized();
                }

                course.IsVisible = !course.IsVisible;
                service.Update(course);

                var courseModel = Mapper.Map<CourseViewModel>(course);

                return Ok(courseModel);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/courses/{id:int}/enrollments")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetEnrollments(int id)
        {
            string userId = User.Identity.GetUserId();
            var user = userService.GetById(userId);

            if (!user.IsTeacher)
            {
                return Unauthorized();
            }

            var course = service.GetById(id);

            if (course != null)
            {
                if (course.TeacherId != userId)
                {
                    return Unauthorized();
                }

                var enrollments = course.Enrollments;
                var enrollmentModels = Mapper.Map<IEnumerable<EnrollmentViewModel>>(enrollments);


                return Ok(enrollmentModels);
            }
            else
            {
                return NotFound();
            }
        }
    }
}