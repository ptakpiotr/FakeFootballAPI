using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeFootball.Data.JwtData
{
    public class JwtUserRepo : IJwtUserRepo
    {
        private readonly JwtUserDbContext _context;

        public JwtUserRepo(JwtUserDbContext context)
        {
            _context = context;
        }
        public void AddOneUser(JwtUserModel juser)
        {
            _context.JwtUsers.Add(juser);
        }

        public JwtUserModel GetOneUser(string name)
        {
            var res = _context.JwtUsers.First(u=>u.Email==name);

            return (res);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
