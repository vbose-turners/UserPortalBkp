using System.Collections.Generic;
using System.Threading.Tasks;
using Turners.UserPortal.Domain;

namespace Turners.UserPortal.Service
{
    public interface IBranchesService
    {
        Dictionary<string, Branch> BranchesDictionary{ get; }

        Task<List<Branch>> GetAllBranches();

        Branch GetBranchByName(string name);
    }
}