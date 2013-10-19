using iShare.DataInterface;
using iShare.Models;

namespace iShare.DataContext.Repositories
{
    public class CauseRepository : Repository<Cause>, ICauseRepository
    {
        public CauseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}