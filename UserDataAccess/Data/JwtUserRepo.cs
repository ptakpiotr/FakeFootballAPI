using JwtUserDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UserDataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace JwtUserDataAccess.Data
{
    class JwtUserRepo : IJwtUserRepo
    {
        private readonly JwtUserDbContext _context;

        public JwtUserRepo(JwtUserDbContext context)
        {
            _context = context;
        }

        public void AddOneUser(JwtUserModel model)
        {
            _context.JwtUsers.Add(model);
        }

        public JwtUserModel GetOneUser(string name)
        {
            var res = _context.JwtUsers.FirstOrDefault(u=>u.Name==name);
            return (res);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
