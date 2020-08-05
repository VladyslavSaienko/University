using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace University.API.Models
{
    public class UpdateLessonDto
    {
        [Required(ErrorMessage = "Please provide the name")]
        [MaxLength(40)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
