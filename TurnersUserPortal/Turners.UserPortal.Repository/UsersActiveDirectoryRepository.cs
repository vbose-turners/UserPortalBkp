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

        public List<User> GetUsers(string userName, params string[] departmentNames)
        {
            var users = new List<User>();
            try
            {
                var userNameQuery = string.IsNullOrEmpty(userName) ? "*" : $"*{userName.Trim()}*";

                userNameQuery = $"(|(SAMAccountName={userNameQuery})(name={ userNameQuery})(cn={userNameQuery}))";

                var departmentNameQuery = "(department=*)";

                if(departmentNames!=null && departmentNames.Any())
                {
                    if(departmentNames.Length == 1)
                    {
                        departmentNameQuery = $"(department=*{departmentNames[0].Trim()}*)";
                    }
                    else
                    {
                        departmentNameQuery = string.Empty;
                        departmentNames.ToList().ForEach(x => departmentNameQuery += $"(department=*{x.Trim()}*)");

                        departmentNameQuery = $"(|{departmentNameQuery})";
                    }
                }

                var query = $"(&(objectClass=user)(objectCategory=person){userNameQuery}{departmentNameQuery})";

                var searcher = new DirectorySearcher(_rootEntry);

                searcher.Filter = query;

                var results = searcher.FindAll().Cast<SearchResult>().ToList();
                users = results.Select(x => new User()
                {
                    Name = GetValue(x, "name"),
                    JobDescription = GetValue(x, "title"),
                    EmailAddress = GetValue(x, "mail"),
                    TelephoneNumber = GetValue(x, "telephoneNumber"),
                    Extension = GetValue(x, "otherTelephone"),
                    Mobile = GetValue(x, "mobile"),
                    Department = GetValue(x, "department"),
                }).ToList();
            }
            catch(Exception e)
            {
                _rootEntry.Close();
                throw new Exception("Error in searching users in active directory: ", e.InnerException);
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
