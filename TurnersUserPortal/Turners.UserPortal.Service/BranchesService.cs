using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Turners.UserPortal.Domain;
using Turners.UserPortal.Helpers;
using Turners.UserPortal.Repository;

namespace Turners.UserPortal.Service
{
    public class BranchesService : IBranchesService
    {
        private readonly IBranchesRepository _branchesRepository;
        private Dictionary<string, Branch> _branchesDictionary;
        private List<Branch> _branches;
        //private static readonly object lockObj = new object();

        public BranchesService(IBranchesRepository branchesRepository)
        {
            _branchesRepository = branchesRepository;
            _branchesDictionary = new Dictionary<string, Branch>();
        }


        public Dictionary<string, Branch> BranchesDictionary
        {
            get
            {
                if (_branchesDictionary == null || !_branchesDictionary.Any())
                {
                    _branchesDictionary = new Dictionary<string, Branch>();

                    
                    Branches.ForEach(x => _branchesDictionary.Add(x.Name.Trim().Sanitize(), x));
                }

                return _branchesDictionary;
            }
        }

        public List<Branch> Branches
        {
            get
            {
                return GetAllBranches().Result;
            }
        }

        public async Task<List<Branch>> GetAllBranches()
        {
            if (_branches == null || !_branches.Any())
            {
                _branches = await _branchesRepository.GetAllBranches();
            }

            return _branches;
        }

        public Branch GetBranchByName(string name)
        {
            name = name.Sanitize();

            if (BranchesDictionary.TryGetValue(name, out Branch branch))
            {
                return branch;
            }

            return null;
        }
    }
}
