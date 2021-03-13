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
    public class DeleteModel : PageModel
    {
        private readonly IUsersRepository _usersRepository;

        public DeleteModel(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [BindProperty]
        public UserModel User { get; set; }

        public IActionResult OnGet(int id)
        {
            User = _usersRepository.GetUser(id);

            if (User == null)
                return RedirectToPage("/NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            UserModel deletedUser = _usersRepository.Delete(User.Id);

            if (deletedUser == null)
                return RedirectToPage("/NotFound");

            return RedirectToPage("Users");
        }
    }
}
