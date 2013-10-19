using iShare.BusinessLogic.Contracts;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.BusinessLogic
{
    public class CategoryService : Service<ICategoryRepository, Category>, ICategoryService
    {
        private ICategoryRepository CategoryRepository { get; set; }

        public CategoryService(ICategoryRepository repository) : base(repository)
        {
            CategoryRepository = repository;
        }
    }
}
