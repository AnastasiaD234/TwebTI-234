using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.DBModel
{
     public class SeshContext : DbContext
     {
          public SeshContext() : base("name=Sarkis")
          {
          }

          public virtual DbSet<Session> Sessions { get; set; }
     }
}
