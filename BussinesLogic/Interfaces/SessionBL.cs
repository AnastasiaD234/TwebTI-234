using BussinesLogic.Core;
using BussinesLogic.Interfaces;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BussinesLogic.DBModel
{
     public class SessionBL : UserAPI, ISession
     {
          public ULoginResp UserLogin(ULoginData data)
          {
               return UserLoginAction(data);
          }

          public URegisterresp UserRegister(URegisterData data)
          {
               return UserRegisterAction(data);  
          }

          public HttpCookie GenCookie(string loginCredential)
          {
               return Cookie(loginCredential);
          }

          public UserMinimal GetUserByCookie(string apiCookieValue)
          {
               return UserCookie(apiCookieValue);
          }

          URole ISession.GetUserByCookie(string apiCookieValue)
          {
               throw new NotImplementedException();
          }
     }
}
