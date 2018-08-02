using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turners.UserPortal.Domain;

namespace Turners.UserPortal.Repository
{
    public class UsersActiveDirectoryRepository : IUserRepository
    {
        private DirectoryEntry _rootEntry;

        public UsersActiveDirectoryRepository()
        {
            _rootEntry = new DirectoryEntry("LDAP://auctions.co.nz", "vbose", "Vb100207");
        }

        public List<User> GetUsers(string userName, string departmentName)
        {
            var users = new List<User>();
            try
            {
                var userNameQuery = string.IsNullOrEmpty(userName) ? "*" : $"*{userName}*";
                var departmentNameQuery = string.IsNullOrEmpty(departmentName) ? "*" : $"*{departmentName}*";

                var query = $"(&(objectClass=user)(objectCategory=person)(|(SAMAccountName={userNameQuery})(name={userNameQuery}))(department={departmentNameQuery}))";

                var searcher = new DirectorySearcher(_rootEntry);

                searcher.Filter = query;

                var results = searcher.FindAll().Cast<SearchResult>().ToList();
                users = results.Select(x => new User()
                {
                    Name = GetValue(x, "name"),
                    JobDescription = GetValue(x, "title"),
                    TelephoneNumber = GetValue(x, "telephoneNumber"),
                    Extension = GetValue(x, "otherTelephone"),
                    Mobile = GetValue(x, "mobile"),
                    Department = GetValue(x, "department"),
                }).ToList();
            }
            catch(Exception e)
            {
                _rootEntry.Close();
                throw new Exception("Ërror in searching users in active directory: ", e.InnerException);
            }
            finally
            {
                _rootEntry.Close();
            }

            return users; 
        }

        private string GetValue(SearchResult result, string fieldName)
        {
            return (result.Properties[fieldName].Count > 0 ? result.Properties[fieldName][0].ToString() : string.Empty);
        }
    }
}
