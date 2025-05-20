using BussinesLogic.DBModel;
using Domain.Entities;
using Domain.Entities.User;
using Domain.Model.User;
using Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BussinesLogic.Core
{
     public class UserAPI
     {
          internal ULoginResp UserLoginAction(ULoginData data)
          {
               UDbTable result;
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Credential))
               {
                    var pass = LoginHelper.HashGen(data.Password);
                    using (var db = new UserContext())
                    {
                         result = db.Users.FirstOrDefault(u => u.Email == data.Credential && u.Password == pass);
                    }

                    if (result == null)
                    {
                         return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                    }

                    using (var todo = new UserContext())
                    {
                         result.LastIp = data.LoginIp;
                         result.LastLogin = data.LoginDateTime;
                         todo.Entry(result).State = EntityState.Modified;
                         todo.SaveChanges();
                    }

                    return new ULoginResp { Status = true };
               }
               else
               {
                    var pass = LoginHelper.HashGen(data.Password);
                    using (var db = new UserContext())
                    {
                         result = db.Users.FirstOrDefault(u => u.Username == data.Credential && u.Password == pass);
                    }

                    if (result == null)
                    {
                         return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                    }

                    using (var todo = new UserContext())
                    {
                         result.LastIp = data.LoginIp;
                         result.LastLogin = data.LoginDateTime;
                         todo.Entry(result).State = EntityState.Modified;
                         todo.SaveChanges();
                    }

                    return new ULoginResp { Status = true };
               }
          }

          internal URegisterResp UserRegisterAction(URegisterData data)
          {
               using (var db = new UserContext())
               {
                    if (db.Users.Any(u => u.Username == data.Username || u.Email == data.Email))
                    {
                         return new URegisterResp { Status = false, StatusMsg = "Username or email already exists." };
                    }

                    var newUser = new UDbTable
                    {
                         Username = data.Username,
                         Email = data.Email,
                         Password = LoginHelper.HashGen(data.Password),
                         LastLogin = DateTime.Now,
                         LastIp = data.RegisterIp,
                         Level = data.Level
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    return new URegisterResp { Status = true, StatusMsg = "Registration successful." };
               }
          }

          internal HttpCookie Cookie(string loginCredential)
          {
               var apiCookie = new HttpCookie("X-KEY")
               {
                    Value = CookieGenerator.Create(loginCredential),
                    Expires = DateTime.Now.AddMinutes(60) // Adaugă și data de expirare
               };

               using (var db = new SessionContext())
               {
                    var curent = db.Sessions.FirstOrDefault(s => s.Username == loginCredential);

                    if (curent != null)
                    {
                         curent.CookieString = apiCookie.Value;
                         curent.ExpireTime = apiCookie.Expires;
                         db.Entry(curent).State = EntityState.Modified;
                    }
                    else
                    {
                         db.Sessions.Add(new Session
                         {
                              Username = loginCredential,
                              CookieString = apiCookie.Value,
                              ExpireTime = apiCookie.Expires
                         });
                    }

                    db.SaveChanges();
               }

               return apiCookie;
          }

          internal UserMinimal UserCookie(string cookie)
          {
               Session session;
               UDbTable curentUser;

               using (var db = new SessionContext())
               {
                    session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
               }

               if (session == null) return null;
               using (var db = new UserContext())
               {
                    var validate = new EmailAddressAttribute();
                    if (validate.IsValid(session.Username))
                    {
                         curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                    }
                    else
                    {
                         curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
                    }
               }

               if (curentUser == null) return null;
               var userMinimal = new UserMinimal
               {
                    Username = curentUser.Username,
                    Email = curentUser.Email,
                    Level = curentUser.Level 
               };

               return userMinimal;
          }

     }
}
