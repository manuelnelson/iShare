using System.Collections.Generic;
using iShare.Models;

namespace iShare.DataInterface
{
    public interface ICharityRepository : IRepository<Charity>
    {
        List<Charity> GetAll();
    }
}
