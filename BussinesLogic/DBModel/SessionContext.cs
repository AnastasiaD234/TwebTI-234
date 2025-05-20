using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.DBModel
{
     public class SessionContext : DbContext
     {
          public SessionContext() : base("name=Sarkis")
          {
          }

          public virtual DbSet<Session> Sessions { get; set; }
     }
}
