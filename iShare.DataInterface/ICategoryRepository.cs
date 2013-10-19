using System.Collections.Generic;
using iShare.Models;

namespace iShare.DataInterface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        List<Category> GetAll();
    }
}
