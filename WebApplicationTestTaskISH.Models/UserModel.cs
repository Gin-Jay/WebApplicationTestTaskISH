using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationTestTaskISH.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public UserRoles? Role { get; set; }
    }
}
