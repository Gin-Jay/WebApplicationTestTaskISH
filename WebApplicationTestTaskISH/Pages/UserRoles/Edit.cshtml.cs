using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationTestTaskISH.Models;
using WebApplicationTestTaskISH.Services;

namespace WebApplicationTestTaskISH.Pages.UserRoles
{
    public class EditModel : PageModel
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(IUsersRepository usersRepository, IWebHostEnvironment webHostEnvironment)
        {
            _usersRepository = usersRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public UserModel User { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public bool Notify { get; set; }
        public string Message { get; set; }

        public IActionResult OnGet(int id)
        {
            User = _usersRepository.GetUser(id);

            if (User == null)
                return RedirectToPage("/NotFound");

            return Page();

        }

        public IActionResult OnPost(UserModel user)
        {
            if (Photo != null)
            {
                if (user.PhotoPath != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", user.PhotoPath);
                    System.IO.File.Delete(filePath);
                }

                user.PhotoPath = ProcessUploadedFile();
            }

            User = _usersRepository.Update(user);

            TempData["SuccessMessage"] = $"Обновление профиля {User.Name} прошло успешно!";

            return RedirectToPage("Users");
        }

        public void OnPostUpdateNotificationPreferences(int id)
        {
            if (Notify)
                Message = "Спасибо, что включили оповещения";
            else
                Message = "Оповещения выключены";

            User = _usersRepository.GetUser(id);
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fs);
                }
            }

            return uniqueFileName;
        }
    }
}
