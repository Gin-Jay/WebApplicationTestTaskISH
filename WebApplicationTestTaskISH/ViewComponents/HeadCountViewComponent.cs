using Microsoft.AspNetCore.Mvc;
using WebApplicationTestTaskISH.Models;
using WebApplicationTestTaskISH.Services;

namespace WebApplicationTestTaskISH.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IUsersRepository _usersRepository;

        public HeadCountViewComponent(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public IViewComponentResult Invoke(UserRoles? userRole = null)
        {
            var result = _usersRepository.UsersCountByRole(userRole);
            return View(result);
        }
    }
}
