using ServiceStack.OrmLite;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.DataContext.OrmLiteRepositories
{
    public class UserOrmLiteRepository : OrmLiteRepository<User>, IUserRepository
    {
        public UserOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public User GetByUserAuthId(int userAuthId)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                return db.FirstOrDefault<User>(u => u.UserAuthId == userAuthId);
            }  
        }
    }
}
