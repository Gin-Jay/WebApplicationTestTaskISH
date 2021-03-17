﻿using System.Collections.Generic;
using WebApplicationTestTaskISH.Models;

namespace WebApplicationTestTaskISH.Services
{
    public interface IUsersRepository
    {
        IEnumerable<UserModel> Search(string searchTerm);
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetUser(int id);
        UserModel Update(UserModel updatedUser);
        UserModel Add(UserModel newUser);
        UserModel Delete(int id);
        IEnumerable<UsersHeadCount> UsersCountByRole(UserRoles? userRoles);
    }
}
