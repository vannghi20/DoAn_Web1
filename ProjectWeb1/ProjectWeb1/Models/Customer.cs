using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWeb1.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Please enter name")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Please enter phone number")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Phone]
        public string CustomerPhone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Passord")]
        public string Password { get; set; }
    }
}
