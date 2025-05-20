using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sarkis.Models
{
     public class AuthViewModel
     {
          public UserLogin LoginModel { get; set; }
          public UserRegister RegisterModel { get; set; }
     }
}