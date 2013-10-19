using iShare.BusinessLogic.Contracts;
using iShare.DataInterface;
using iShare.Models;

namespace iShare.BusinessLogic
{
    public class CauseService : Service<ICauseRepository, Cause>, ICauseService
    {
        private ICauseRepository CauseRepository { get; set; }

        public CauseService(ICauseRepository repository) : base(repository)
        {
            CauseRepository = repository;
        }
    }
}
