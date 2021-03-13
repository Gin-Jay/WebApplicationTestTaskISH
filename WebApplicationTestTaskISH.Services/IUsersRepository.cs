using System;
using System.Collections.Generic;
using System.Text;
using WebApplicationTestTaskISH.Models;

namespace WebApplicationTestTaskISH.Services
{
    public interface IUsersRepository
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetUser(int id);
        UserModel Update(UserModel updatedUser);
        UserModel Add(UserModel newUser);
        UserModel Delete(int id);
    }
}
