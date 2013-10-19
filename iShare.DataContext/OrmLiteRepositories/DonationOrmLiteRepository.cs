using ServiceStack.OrmLite;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.DataContext.OrmLiteRepositories
{
    public class DonationOrmLiteRepository : OrmLiteRepository<Donation>, IDonationRepository
    {
        public DonationOrmLiteRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
