using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
     public class URegisterData
     {
          public string Username { get; set; }
          public string Email { get; set; }
          public string Password { get; set; }
          public DateTime RegisterDateTime { get; set; }
          public string RegisterIp { get; set; }
          public LevelAcces Level { get; set; }
     }
}
