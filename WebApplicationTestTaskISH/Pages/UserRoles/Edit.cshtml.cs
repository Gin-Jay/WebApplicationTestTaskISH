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

        [BindProperty]
        public UserModel User { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public bool Notify { get; set; }
        public string Message { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
                User = _usersRepository.GetUser(id.Value);
            else
                User = new UserModel();

            if (User == null)
                return RedirectToPage("/NotFound");

            return Page();

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (User.PhotoPath != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", User.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    User.PhotoPath = ProcessUploadedFile();
                }

                if (User.Id > 0)
                {
                    User = _usersRepository.Update(User);
                    TempData["SuccessMessage"] = $"Обновление профиля {User.Name} прошло успешно!";
                }
                else
                {
                    User = _usersRepository.Add(User);
                    TempData["SuccessMessage"] = $"Добавление профиля {User.Name} прошло успешно!";
                }

                return RedirectToPage("Users");
            }

            return Page();
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
