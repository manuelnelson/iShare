using iShare.DataInterface;
using iShare.Models;

namespace iShare.DataContext.Repositories
{
    public class CharityRepository : Repository<Charity>, ICharityRepository
    {
        public CharityRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}