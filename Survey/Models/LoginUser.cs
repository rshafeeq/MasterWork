using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Survey.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage ="Please enter UserName")]
        [Display(Name = "Enter User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        [Display(Name = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Roles { get; set; }
        public int RoleId { get; set; }
        public int FullName { get; set; }
    }

    
}