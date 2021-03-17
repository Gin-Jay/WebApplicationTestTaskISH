using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplicationTestTaskISH.Models;

namespace WebApplicationTestTaskISH.Services
{
    public class MockUsersRepository : IUsersRepository
    {
        private List<UserModel> _usersList;

        public MockUsersRepository()
        {
            _usersList = new List<UserModel>
            {
                new UserModel()
                {
                    Id = 0, Name = "Stew", Email = "stew@example.com", PhotoPath = "avatar1.png", Role = UserRoles.Admin
                },
                new UserModel()
                {
                    Id = 1, Name = "Kilroy", Email = "kilroy@example.com", PhotoPath = "avatar2.png", Role = UserRoles.Admin
                },
                new UserModel()
                {
                    Id = 2, Name = "Ken", Email = "ken@example.com", PhotoPath = "avatar3.png", Role = UserRoles.User
                },
                new UserModel()
                {
                    Id = 3, Name = "Lika", Email = "lika@example.com", PhotoPath = "avatar4.png", Role = UserRoles.User
                },
                new UserModel()
                {
                    Id = 4, Name = "Jay", Email = "Jay@example.com", PhotoPath = "avatar5.png", Role = UserRoles.User
                },
                new UserModel()
                {
                    Id = 5, Name = "Luvs", Email = "luvs@example.com", Role = UserRoles.User
                }
            };
        }

        public UserModel Add(UserModel newUser)
        {
            //ver1
            //if (_usersList.Count != 0)
            //    newUser.Id = _usersList.Max(x => x.Id) + 1;
            //else
            //    newUser.Id = 0;
            //newUser.Id = _usersList.Count != 0 ? _usersList.Max(x => x.Id) + 1 : 0; //ver2
            newUser.Id = (_usersList.Max(x => (int?)x.Id) ?? -1) + 1; //ver3
            _usersList.Add(newUser);
            return newUser;
        }

        public UserModel Delete(int id)
        {
            UserModel userToDelete = _usersList.FirstOrDefault(x => x.Id == id);
            if (userToDelete != null)
                _usersList.Remove(userToDelete);

            return userToDelete;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _usersList;
        }

        public UserModel GetUser(int id)
        {
            return _usersList.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<UserModel> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _usersList;

            return _usersList.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) || x.Email.ToLower().Contains(searchTerm.ToLower()));
        }

        public UserModel Update(UserModel updatedUser)
        {
            UserModel user = _usersList.FirstOrDefault(x => x.Id == updatedUser.Id);

            if (user != null)
            {
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.Role = updatedUser.Role;
                user.PhotoPath = updatedUser.PhotoPath;
            }

            return user;
        }

        public IEnumerable<UsersHeadCount> UsersCountByRole(UserRoles? userRoles)
        {
            IEnumerable<UserModel> querry = _usersList;

            if (userRoles.HasValue)
                querry = querry.Where(x => x.Role == userRoles.Value);

            return querry.GroupBy(x => x.Role)
                             .Select(x => new UsersHeadCount()
                             {
                                 Role = x.Key.Value,
                                 Count = x.Count()
                             }).ToList();
        }
    }
}
