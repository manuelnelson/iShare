using iShare.DataInterface;
using iShare.Models;

namespace iShare.BusinessLogic.Contracts
{
    public interface IUserService : IService<IUserRepository, User>
    {
        User CreateOrUpdate(User user);
    }
}
