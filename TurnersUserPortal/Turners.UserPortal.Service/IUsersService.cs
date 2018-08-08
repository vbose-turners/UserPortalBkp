using System.Collections.Generic;
using Turners.UserPortal.Domain;

namespace Turners.UserPortal.Service
{
    public interface IUsersService
    {
        List<User> GetUsers(string userName, string departmentName);
    }
}