using System.Collections.Generic;
using Turners.UserPortal.Domain;

namespace Turners.UserPortal.Service
{
    public interface IBranchesService
    {
        IDictionary<string, Branch> Branches { get; }

        Branch GetBranchByName(string name);
    }
}