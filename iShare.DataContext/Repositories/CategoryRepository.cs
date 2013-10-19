using System.Collections.Generic;
using System.Linq;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.DataContext.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<Category> GetAll()
        {
            return GetDbSet().ToList();
        }
    }
}