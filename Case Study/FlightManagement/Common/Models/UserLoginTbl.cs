using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Models
{
     public class UserLoginTbl
    {

        public string UserName { get; set; }
        
        public string Password { get; set; }
    }
}
