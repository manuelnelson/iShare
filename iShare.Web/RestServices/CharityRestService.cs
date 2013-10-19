using System.Net;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
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
            public string Name { get; set; }
            public string Url { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public int OrgId { get; set; }
            public long CategoryId { get; set; }
            public string Cause { get; set; }
            public string TagLine { get; set; }
            public string Summary { get; set; }
            public int Rating { get; set; }
            public int Score { get; set; }
        }

        public class CharitiesService : ServiceBase
        {
            public ICharityService CharityService { get; set; } //Injected by IOC

            public object Get(CharityDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    return CharityService.Get(request.Ids);
                if (request.Id > 0)
                    return CharityService.Get(request.Id);
                return CharityService.GetAll();
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
