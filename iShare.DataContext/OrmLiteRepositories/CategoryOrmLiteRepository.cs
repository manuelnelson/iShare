using ServiceStack.OrmLite;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.DataContext.OrmLiteRepositories
{
    public class CategoryOrmLiteRepository : OrmLiteRepository<Category>, ICategoryRepository
    {
        public CategoryOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
