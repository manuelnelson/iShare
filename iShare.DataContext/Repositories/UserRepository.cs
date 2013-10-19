using System.Linq;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.DataContext.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public User GetByUserAuthId(int userAuthId)
        {
            return GetDbSet().FirstOrDefault(u => u.UserAuthId == userAuthId);
        }
    }
}