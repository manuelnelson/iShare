using System;
using System.Collections.Generic;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.DataContext.Repositories
{
    public class CharityRepository : Repository<Charity>, ICharityRepository
    {
        public CharityRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<Charity> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}