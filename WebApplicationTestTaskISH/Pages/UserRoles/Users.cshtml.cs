using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationTestTaskISH.Models;
using WebApplicationTestTaskISH.Services;

namespace WebApplicationTestTaskISH.Pages.UserRoles
{
    public class UsersModel : PageModel
    {
        private readonly IUsersRepository _db;

        public UsersModel(IUsersRepository db)
        {
            _db = db;
        }

        public IEnumerable<WebApplicationTestTaskISH.Models.UserModel> Users { get; set; }

        public void OnGet()
        {
            Users = _db.GetAllUsers();
        }
    }
}
