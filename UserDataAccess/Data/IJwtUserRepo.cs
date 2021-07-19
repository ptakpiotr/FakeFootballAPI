using JwtUserDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtUserDataAccess.Data
{
    public interface IJwtUserRepo
    {
        JwtUserModel GetOneUser(string name);
        void AddOneUser(JwtUserModel model);
        void SaveChanges();
    }
}
