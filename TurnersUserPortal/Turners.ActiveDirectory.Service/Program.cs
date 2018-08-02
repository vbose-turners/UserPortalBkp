using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turners.UserPortal.Repository;

namespace Turners.UserPortal.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //DirectoryEntry rootEntry = new DirectoryEntry("LDAP://auctions.co.nz", "vbose", "Vb100207");

            //var query  = "(&(objectClass=user)(objectCategory=person)(|(SAMAccountName=*paul*)(cn=*paul*)))";

            //var searcher = new DirectorySearcher(rootEntry);

            //searcher.Filter = query;

            //foreach (SearchResult result in searcher.FindAll())
            //{
            //    Console.WriteLine("account name: {0}", result.Properties["samaccountname"].Count > 0 ? result.Properties["samaccountname"][0] : string.Empty);
            //    Console.WriteLine("department name: {0}", result.Properties["department"].Count > 0 ? result.Properties["department"][0] : string.Empty);
            //    Console.WriteLine("Name: {0}", result.Properties["cn"].Count > 0 ? result.Properties["cn"][0] : string.Empty);
            //    Console.WriteLine("Job description: {0}", result.Properties["title"].Count > 0 ? result.Properties["title"][0] : string.Empty);
            //}

            //rootEntry.Close();


            var repo = new UsersActiveDirectoryRepository();

            var users = repo.GetUsers("bose", string.Empty);

            foreach (var user in users)
            {
                Console.WriteLine("account name: {0}", user.Name);
                Console.WriteLine("department name: {0}",user.Department);
                Console.WriteLine("Job description: {0}", user.JobDescription);
            }

            var branchesSvc = new BranchesService(new BranchesCsvRepository());
            var branches = branchesSvc.Branches;

            foreach (var x in branches.Keys)
            {
                var branch = branchesSvc.GetBranchByName(x);

                Console.WriteLine("branch name: {0}", branch.Name);
                Console.WriteLine("branch Aliases: {0}", branch.Aliases);
                Console.WriteLine("branch Address: {0}", branch.Address);

                Console.WriteLine("\n\n");
            }

            Console.Read();
        }
    }
}
