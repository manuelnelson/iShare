using System.Net;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using iShare.BusinessLogic.Contracts;
using iShare.Models;

namespace iShare.Web.RestServices
{
    public class CharityRestService
    {
        [Route("/Charities", "POST")]
        [Route("/Charities", "PUT")]
        [Route("/Charities", "GET")]
        [Route("/Charities", "DELETE")]
        [Route("/Charities")]
        [Route("/Charities/{Id}")]
        public class CharityDto : IReturn<CharityDto>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
        }

        public class CharitiesService : Service
        {
            public ICharityService CharityService { get; set; } //Injected by IOC

            public object Get(CharityDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    return CharityService.Get(request.Ids);
                if (request.Id > 0)
                    return CharityService.Get(request.Id);
                throw new HttpError(HttpStatusCode.BadRequest, "Invalid argument(s) supplied.");
            }

            public object Put(CharityDto request)
            {
                var CharityEntity = request.TranslateTo<Charity>();
                CharityService.Update(CharityEntity);
                return CharityEntity;
            }

            public object Post(CharityDto request)
            {
                var CharityEntity = request.TranslateTo<Charity>();
                CharityService.Add(CharityEntity);
                return CharityEntity;
            }

            public void Delete(CharityDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    CharityService.DeleteAll(request.Ids);
                else
                    CharityService.Delete(request.Id);
            }
        }

    }

}
