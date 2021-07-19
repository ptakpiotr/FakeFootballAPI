using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JwtUserDataAccess.Models
{
    public class JwtUserModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set {
                _name = Email.Substring(0, Email.IndexOf("@"));
            }
        }

        public string Password { get; set; }
    }
}
