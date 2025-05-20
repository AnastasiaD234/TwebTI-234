using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BussinesLogic.DBModel
{
     public class UserContext : DbContext
     {
          public UserContext() :  base("name=Sarkis")
          {
          }

          public  DbSet<UDbTable> Users { get; set; }
     }
}

