using System.Collections.Generic;
using System.Threading.Tasks;
using Turners.UserPortal.Domain;

namespace Turners.UserPortal.Repository
{
    public interface IBranchesRepository
    {
        Task<List<Branch>> GetAllBranches();
    }
}