using iShare.BusinessLogic.Contracts;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.BusinessLogic
{
    public class DonationService : Service<IDonationRepository, Donation>, IDonationService
    {
        private IDonationRepository DonationRepository { get; set; }

        public DonationService(IDonationRepository repository) : base(repository)
        {
            DonationRepository = repository;
        }
    }
}
