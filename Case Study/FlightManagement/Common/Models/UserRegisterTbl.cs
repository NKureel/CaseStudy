using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Models
{
    public class UserRegisterTbl
    {
        [Required(ErrorMessage ="User Name is required")]
       public string UserName { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}
