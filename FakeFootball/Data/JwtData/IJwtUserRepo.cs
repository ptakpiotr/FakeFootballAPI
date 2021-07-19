using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeFootball.Models;

namespace FakeFootball.Data.JwtData
{
    public interface IJwtUserRepo
    {
        JwtUserModel GetOneUser(string name);
        void AddOneUser(JwtUserModel juser);
        void SaveChanges();
    }
}
