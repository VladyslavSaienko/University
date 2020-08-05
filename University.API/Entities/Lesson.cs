using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace University.API.Entities
{
    public class Lesson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public int CourseId { get; set; }

    }
}
