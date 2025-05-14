using BussinesLogic.BLStructure;
using BussinesLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic
{
     public class BussinesLogic
     {
          public IAuth GetAuthBL()
          {
               return new AuthBL();
          }
     }
}
