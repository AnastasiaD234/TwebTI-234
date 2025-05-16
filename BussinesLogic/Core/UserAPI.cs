using BussinesLogic.DBModel;
using Domain.Entities.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace BussinesLogic.Core
{
     public class UserAPI
     {
          internal ULoginResp UserLoginAction(ULoginData data) 
          {
               UDbTable result;
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Username))
               {
                    var pass = Hash.HashGen(data.Password);
                    using (var db = new UserContext())
                    {
                         result = db.Users.FirstOrDefault(u => u.Email == data.Username && u.Password == pass);
                    }

                    if (result == null)
                    {
                         return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                    }

                    using (var todo = new UserContext())
                    {
                       
                         todo.Entry(result).State = EntityState.Modified;
                         todo.SaveChanges();
                    }

                    return new ULoginResp { Status = true };
               }
               else
               {
                    var pass = Hash.HashGen(data.Password);
                    using (var db = new UserContext())
                    {
                         result = db.Users.FirstOrDefault(u => u.Username == data.Username && u.Password == pass);
                    }

                    if (result == null)
                    {
                         return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                    }

                    using (var todo = new UserContext())
                    {
                         todo.Entry(result).State = EntityState.Modified;
                         todo.SaveChanges();
                    }

                    return new ULoginResp { Status = true };
               }
          }

          internal URegisterresp UserRegisterAction(URegisterData data)
          {
               using (var db = new UserContext())
               {
                    if (db.Users.Any(u => u.Username == data.Username || u.Email == data.Email))
                    {
                         return new URegisterresp { Status = false, StatusMsg = "Username or email already exists." };
                    }

                    var newUser = new UDbTable
                    {
                         Username = data.Username,
                         Email = data.Email,
                         Password = Hash.HashGen(data.Password),
           
                         Level = 0
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    return new URegisterresp { Status = true, StatusMsg = "Registration successful." };
               }
          }

          public HttpCookie Cookie(string loginCredential)
          {
               var apiCookie = new HttpCookie("X-KEY")
               {
                    Value = Cookie.Create(loginCredential)
               };

               using (var db = new SeshContext())
               {
                    Session curent;
                    var validate = new EmailAddressAttribute();
                    if (validate.IsValid(loginCredential))
                    {
                         curent = db.Sessions.FirstOrDefault(e => e.Username == loginCredential);
                    }
                    else
                    {
                         curent = db.Sessions.FirstOrDefault(e => e.Username == loginCredential);
                    }

                    if (curent != null)
                    {
                         curent.CookieString = apiCookie.Value;
                         curent.ExpireTime = DateTime.Now.AddMinutes(60);
                         using (var todo = new SeshContext())
                         {
                              todo.Entry(curent).State = EntityState.Modified;
                              todo.SaveChanges();
                         }
                    }
                    else
                    {
                         db.Sessions.Add(new Session
                         {
                              Username = loginCredential,
                              CookieString = apiCookie.Value,
                              ExpireTime = DateTime.Now.AddMinutes(60)
                         });
                         db.SaveChanges();
                    }
               }

               return apiCookie;
          }

          internal UserMinimal UserCookie(string cookie)
          {
               Session session;
               UDbTable curentUser;

               using (var db = new SeshContext())
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
               var userminimal = Mapper.Map<UserMinimal>(curentUser);

               return userminimal;
          }
     }
}

