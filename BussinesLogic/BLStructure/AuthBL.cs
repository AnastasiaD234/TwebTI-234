using BussinesLogic.Core;
using BussinesLogic.Interfaces;
using Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.BLStructure
{
     public class AuthBL : UserAPI, IAuth
     {
          public string UserAuthLogic(UserLoginDTD data)
          {
               return UserAuthLogicAction(data);
          }
     }
}
