using System.Collections.Generic;
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

        public List<Category> GetAll()
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.Select<Category>();
                //return db.Select<Category>("SELECT * FROM Categories");
            }  
        }
    }
}
