using BussinesLogic.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesLogic.Interfaces;

namespace BussinesLogic.Interfaces
{
     public class BussinesLogic
     {
          public ISession GetSessionBL()
          {
               return new SessionBL();
          }
     }
}
