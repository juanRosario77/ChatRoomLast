using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Name must be at least {2}, and maximum {1} characters")]
        public string Name { get; set; }

        //[Required]
        //[StringLength(100, MinimumLength = 1, ErrorMessage = "Enter password")]
        //public string Password { get; set; }

        //[EmailAddress]
        //public string Email { get; set; }

    }
}
