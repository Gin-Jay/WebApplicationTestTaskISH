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
    public class DetailsModel : PageModel
    {
        private readonly IUsersRepository _usersRepository;

        public DetailsModel(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public new UserModel User { get; private set; }

        public IActionResult OnGet(int id)
        {
            User = _usersRepository.GetUser(id);

            if (User == null)
                return RedirectToPage("/NotFound");

            return Page();
        }
    }
}
