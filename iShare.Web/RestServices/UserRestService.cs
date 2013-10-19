using System.Net;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using iShare.BusinessLogic.Contracts;
using iShare.Models;

namespace iShare.Web.RestServices
{
    public class UserRestService
    {
        [Route("/Users", "POST")]
        [Route("/Users", "PUT")]
        [Route("/Users", "GET")]
        [Route("/Users", "DELETE")]
        [Route("/Users")]
        [Route("/Users/{Id}")]
        public class UserDto : IReturn<UserDto>
        {
            public long Id { get; set; }
            public long[] Ids { get; set; }
        }

        public class UsersService : ServiceBase
        {
            public IUserService UserService { get; set; } //Injected by IOC

            public object Get(UserDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    return UserService.Get(request.Ids);
                if (request.Id > 0)
                    return UserService.Get(request.Id);
                throw new HttpError(HttpStatusCode.BadRequest, "Invalid argument(s) supplied.");
            }

            public object Put(UserDto request)
            {
                var UserEntity = request.TranslateTo<User>();
                UserService.Update(UserEntity);
                return UserEntity;
            }

            public object Post(UserDto request)
            {
                var UserEntity = request.TranslateTo<User>();
                UserService.Add(UserEntity);
                return UserEntity;
            }

            public void Delete(UserDto request)
            {
                if (request.Ids != null && request.Ids.Length > 0)
                    UserService.DeleteAll(request.Ids);
                else
                    UserService.Delete(request.Id);
            }
        }

    }

}
