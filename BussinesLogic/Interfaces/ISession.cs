using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BussinesLogic.Interfaces
{
     public interface ISession
     {
          ULoginResp UserLogin(ULoginData data);
          URegisterresp UserRegister(URegisterData data);
          HttpCookie GenCookie(string username);
          URole GetUserByCookie(string apiCookieValue);
     }

}
