using iShare.DataInterface;
using iShare.Models;

namespace iShare.DataContext.Repositories
{
    public class DonationRepository : Repository<Donation>, IDonationRepository
    {
        public DonationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}