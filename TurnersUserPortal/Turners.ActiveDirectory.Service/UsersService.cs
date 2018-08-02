using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turners.UserPortal.Domain;
using Turners.UserPortal.Repository;

namespace Turners.UserPortal.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _repository;

        public UsersService(IUserRepository repository)
        {
            _repository = repository;
        }

        public List<User> GetUsers(string userName, string departmentName)
        {

            userName = Sanitize(userName);
            departmentName = Sanitize(departmentName);

            var users = _repository.GetUsers(userName, departmentName);

            users = users.OrderBy(u => u.Name).ToList();

            return users;
        }

        private string Sanitize(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return str.Replace("*", "")
                    .Replace("/", "")
                    .Replace("\\", "")
                    .Replace("=", "")
                    .Replace("<", "")
                    .Replace(">", "")
                    .Replace("+", "")
                    .Replace("-", "")
                    .Replace("&", "")
                    .Replace("|", "")
                    .Replace("!", "")
                    .Replace("$", "")
                    .Replace("%", "")
                    .Replace("^", "")
                    .Replace("#", "")
                    .Replace("@", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace(":", "")
                    .Replace(";", "")
                    .Replace("\"", "")
                    .Replace("'", "")
                    .Replace("?", "")
                    .Replace(",", "");
        }
    }
}
