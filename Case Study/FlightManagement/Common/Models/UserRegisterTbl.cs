using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Models
{
    public class UserRegisterTbl
    {        
       public string UserName { get; set; }

        
        public string Password { get; set; }
        
        public string Email { get; set; }
    }
}
