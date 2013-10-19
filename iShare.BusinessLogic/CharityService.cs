using System;
using System.Collections.Generic;
using Elmah;
using iShare.BusinessLogic.Contracts;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.BusinessLogic
{
    public class CharityService : Service<ICharityRepository, Charity>, ICharityService
    {
        private ICharityRepository CharityRepository { get; set; }

        public CharityService(ICharityRepository repository) : base(repository)
        {
            CharityRepository = repository;
        }

        public List<Charity> GetAll()
        {
            try
            {
                return CharityRepository.GetAll();
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception("Unable to retrieve information", ex);
            }
        }
    }
}
