namespace LearnToLearn.Rest.App_Start
{
    using AutoMapper;

    using Entities;

    using Models;

    public class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>()
                    .ForMember("TeacherName", opt => opt.MapFrom(c => c.Teacher.UserName));
                cfg.CreateMap<Enrollment, EnrollmentViewModel>()
                    .ForMember("StudentName", opt => opt.MapFrom(e => e.User.UserName))
                    .ForMember("CourseName", opt => opt.MapFrom(e => e.Course.Name));
            });
        }
    }
}