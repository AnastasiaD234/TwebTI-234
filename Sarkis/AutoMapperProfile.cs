using AutoMapper;
using Domain.Entities.User;
using Sarkis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class AutoMapperProfile : Profile
{
     public AutoMapperProfile()
     {
          CreateMap<UserLogin, ULoginData>();
          CreateMap<UserRegister, URegisterData>();
          CreateMap<UDbTable, UserMinimal>();
     }
}