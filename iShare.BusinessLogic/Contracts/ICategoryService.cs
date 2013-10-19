using iShare.DataInterface;
using iShare.Models;

namespace iShare.BusinessLogic.Contracts
{
    public interface ICategoryService : IService<ICategoryRepository, Category>
    {
    }
}
