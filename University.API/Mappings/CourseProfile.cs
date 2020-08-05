using AutoMapper;
using University.API.Entities;
using University.API.Models;

namespace University.API.Mappings
{
    public class CourseProfile:Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseWithoutLessonsDto>();
            CreateMap<Course, CourseDto>();
        }
    }
}
