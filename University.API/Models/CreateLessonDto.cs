using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.API.Models
{
    public class CreateLessonDto
    {
        [Required(ErrorMessage = "Please provide the name")]
        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }
    }
}
