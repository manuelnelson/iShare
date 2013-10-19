using iShare.Models;

namespace iShare.DataInterface
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUserAuthId(int userAuthId);
    }
}
