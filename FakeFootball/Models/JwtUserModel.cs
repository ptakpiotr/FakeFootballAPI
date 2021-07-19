using System;
using System.ComponentModel.DataAnnotations;

namespace FakeFootball.Data
{
    public class JwtUserModel
    {
        [Key]
        public Guid Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}