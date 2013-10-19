using ServiceStack;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.BusinessLogic.Contracts
{
    public interface ICauseService : IService<ICauseRepository, Cause>
    {
    }
}
