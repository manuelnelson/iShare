using iShare.DataInterface;
using iShare.Models;

namespace iShare.BusinessLogic.Contracts
{
    public interface IDonationService : IService<IDonationRepository, Donation>
    {
    }
}
