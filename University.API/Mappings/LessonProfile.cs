using AutoMapper;
using University.API.Entities;
using University.API.Models;

namespace University.API.Mappings
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<Lesson, LessonDto>();
            CreateMap<CreateLessonDto, Lesson>();
            CreateMap<UpdateLessonDto, Lesson>().ReverseMap();
        }
    }
}
