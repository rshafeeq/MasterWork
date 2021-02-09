using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Survey.Models
{
    public class ManageUsers
    {

        [Required(ErrorMessage = "Please Select Role")]
        [Display(Name = " Select Role")]
        public int RoleId { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        
        [Required(ErrorMessage = "Please enter User Name")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter Middle Name")]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Please enter Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter Phone")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

    }
}