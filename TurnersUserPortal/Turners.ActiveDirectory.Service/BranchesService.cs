using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turners.UserPortal.Domain;
using Turners.UserPortal.Repository;

namespace Turners.UserPortal.Service
{
    public class BranchesService : IBranchesService
    {
        private readonly IBranchesRepository _branchesRepository;
        IDictionary<string, Branch> _branches;

        public BranchesService(IBranchesRepository branchesRepository)
        {
            _branchesRepository = branchesRepository;
        }


        public IDictionary<string, Branch> Branches
        {
            get
            {
                if (_branches == null || !_branches.Any())
                {
                    var allBranches = _branchesRepository.GetAllBranches().GetAwaiter().GetResult();

                    _branches = new Dictionary<string, Branch>();

                    allBranches.ForEach(x =>
                        {
                            _branches.Add(x.Name, x);
                        }
                    );
                }

                return _branches;
            }
        }

        public Branch GetBranchByName(string name)
        {
            if(Branches.TryGetValue(name, out Branch branch))
            {
                return branch;
            }

            return null;
        }
    }
}
