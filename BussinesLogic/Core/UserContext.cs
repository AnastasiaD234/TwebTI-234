using System;

namespace BusinessLogic.Core
{
     internal class UserContext : IDisposable
     {
          public object Users { get; internal set; }
     }
}