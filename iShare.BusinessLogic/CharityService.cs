using iShare.BusinessLogic.Contracts;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.BusinessLogic
{
    public class CharityService : Service<ICharityRepository, Charity>, ICharityService
    {
        private ICharityRepository CharityRepository { get; set; }

        public CharityService(ICharityRepository repository) : base(repository)
        {
            CharityRepository = repository;
        }
    }
}
