using System.Collections.Generic;
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

        public IEnumerable<UserModel> Users { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public void OnGet()
        {
            Users = _db.Search(SearchTerm);
        }
    }
}
