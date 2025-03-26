using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechPortal.Models.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
