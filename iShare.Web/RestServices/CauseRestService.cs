using System.Net;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using iShare.BusinessLogic.Contracts;
using iShare.Models;

namespace iShare.Web.RestServices
{
    public class CauseRestService
    {
        [Route("/Causes", "POST")]
        [Route("/Causes", "PUT")]
        [Route("/Causes", "GET")]
        [Route("/Causes", "DELETE")]
        [Route("/Causes")]
        [Route("/Causes/{Id}")]
        public class CauseDto : IReturn<CauseDto>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
            public string Name { get; set; }
        }

        public class CausesService : ServiceBase
        {
            public ICauseService CauseService { get; set; } //Injected by IOC

            public object Get(CauseDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    return CauseService.Get(request.Ids);
                if (request.Id > 0)
                    return CauseService.Get(request.Id);
                throw new HttpError(HttpStatusCode.BadRequest, "Invalid argument(s) supplied.");
            }

            public object Put(CauseDto request)
            {
                var CauseEntity = request.TranslateTo<Cause>();
                CauseService.Update(CauseEntity);
                return CauseEntity;
            }

            public object Post(CauseDto request)
            {
                var CauseEntity = request.TranslateTo<Cause>();
                CauseService.Add(CauseEntity);
                return CauseEntity;
            }

            public void Delete(CauseDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    CauseService.DeleteAll(request.Ids);
                else
                    CauseService.Delete(request.Id);
            }
        }

    }

}
