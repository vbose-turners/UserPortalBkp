using System.Collections.Generic;
using Turners.UserPortal.Domain;

namespace Turners.UserPortal.Repository
{
    public interface IUserRepository
    {
        List<User> GetUsers(string userName, string departmentName);
    }
}