using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using University.API.Interfaces;
using University.API.Models;

namespace University.API.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses = _courseRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<CourseWithoutLessonsDto>>(courses));
        }

        [HttpGet("{id}")]
        public IActionResult GetCourse(int id, bool includeLessons = false)
        {
            var course = _courseRepository.Get(id, includeLessons);

            if (course == null)
            {
                return NotFound();
            }
            if (includeLessons)
            {
                return Ok(_mapper.Map<CourseDto>(course));
            }

            return Ok(_mapper.Map<CourseWithoutLessonsDto>(course));
        }
    }
}
