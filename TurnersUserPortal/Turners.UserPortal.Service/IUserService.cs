using System.Collections.Generic;
using Turners.UserPortal.Domain;

namespace Turners.UserPortal.Service
{
    public interface IUserService
    {
        List<User> GetUsers(string userName, string departmentName);
    }
}