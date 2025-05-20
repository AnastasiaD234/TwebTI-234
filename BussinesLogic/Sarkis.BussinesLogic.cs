
using BussinesLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sarkis.BussinesLogic
{
     public class LogicBussines
     {
          public ISession GetSessionBL()
          {
               return new SessionBL();
          }
     }
}
