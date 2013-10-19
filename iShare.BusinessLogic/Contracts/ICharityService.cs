using iShare.DataInterface;
using iShare.Models;

namespace iShare.BusinessLogic.Contracts
{
    public interface ICharityService : IService<ICharityRepository, Charity>
    {
    }
}
