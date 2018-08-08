using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turners.UserPortal.Domain;
using Turners.UserPortal.Helpers;
using Turners.UserPortal.Repository;

namespace Turners.UserPortal.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _repository;
        private readonly IBranchesService _branchesService;

        public UsersService(IUserRepository repository, IBranchesService branchesService)
        {
            _repository = repository;
            _branchesService = branchesService;
        }

        public List<User> GetUsers(string userName, string departmentName)
        {

            userName = userName.Sanitize();
            departmentName = departmentName.Sanitize();

            var branch = _branchesService.GetBranchByName(departmentName);

            var departmentNameAndAliases = new List<string>() { departmentName};

            if (branch != null && !string.IsNullOrEmpty(branch.Aliases))
            {
                var aliases = branch.Aliases.Trim('"').SplitCSV().ToList();

                aliases.ForEach(x => departmentNameAndAliases.Add(x.Trim()));
            }

            departmentNameAndAliases = departmentNameAndAliases.Where(x => !string.IsNullOrEmpty(x.Trim())).ToList();

            var users = _repository.GetUsers(userName, departmentNameAndAliases.ToArray());

            users = users.OrderBy(u => u.Name).ToList();

            return users;
        }
    }
}
