using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.FluentValidation;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.ServiceModel;
using iShare.BusinessLogic.Contracts;
using iShare.Models;
using iShare.Web.Models;

namespace iShare.Web.RestServices
{
    #region UserAuth
    [Route("/userauths")]
    [DataContract]
    public class UserAuthRequest
    {
        [DataMember]
        public long[] UserIds { get; set; }
        [DataMember]
        public long UserId { get; set; }
        [DataMember]
        public string UserAuthId { get; set; }
    }
    [DataContract]
    public class UserAuthResponse
    {
        [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public virtual string CustomId { get; set; }
        [DataMember]
        public virtual string UserName { get; set; }
        [DataMember]
        public virtual string Email { get; set; }
        [DataMember]
        public virtual string PrimaryEmail { get; set; }
        [DataMember]
        public virtual string FirstName { get; set; }
        [DataMember]
        public virtual string LastName { get; set; }
        [DataMember]
        public virtual string DisplayName { get; set; }
        [DataMember]
        public virtual DateTime? BirthDate { get; set; }
        [DataMember]
        public virtual string BirthDateRaw { get; set; }
        [DataMember]
        public virtual string Country { get; set; }
        [DataMember]
        public virtual string FullName { get; set; }
        [DataMember]
        public virtual string Gender { get; set; }
        [DataMember]
        public virtual string MailAddress { get; set; }
        [DataMember]
        public virtual List<string> Roles { get; set; }
        [DataMember]
        public virtual List<string> Permissions { get; set; }
        [DataMember]
        public virtual DateTime CreatedDate { get; set; }
        [DataMember]
        public virtual DateTime ModifiedDate { get; set; }
    }
    public class UserAuthsResponse
    {
        public UserAuthsResponse()
        {
            this.UserAuths = new List<UserAuth>();
            this.OAuthProviders = new List<UserOAuthProvider>();
        }

        public List<UserAuth> UserAuths { get; set; }
        public CustomUserSession UserSession { get; set; }

        public List<UserOAuthProvider> OAuthProviders { get; set; }
    }
    #endregion

    #region Session
    [Route("/usersession")]
    public class UserSession
    {

    }
    #endregion

    #region Roles
    [Route("/userauths/addroles")]
    public class UserRoles
    {
        public long UserId { get; set; }
        public string[] Roles { get; set; }
        public string UserName { get; set; }
    }
    #endregion
    /// <summary>
    /// Note: We Rolled our own Register Feature For instances When a Logged in User (typically admin) registers a new user.  Servicestack's registration updates the logged in user, which we 
    /// obviously don't want in this scenario.
    /// </summary>
    #region Registration
    public class RegistrationValidator : AbstractValidator<Registration>
    {
        public IUserAuthRepository UserAuthRepo { get; set; }

        public RegistrationValidator()
        {
            RuleSet(ApplyTo.Post, () =>
            {
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty().When(x => x.Email.IsNullOrEmpty());
                RuleFor(x => x.Email).NotEmpty().EmailAddress().When(x => x.UserName.IsNullOrEmpty());
                RuleFor(x => x.UserName)
                    .Must(x => UserAuthRepo.GetUserAuthByUserName(x) == null)
                    .WithErrorCode("AlreadyExists")
                    .WithMessage("UserName already exists")
                    .When(x => !x.UserName.IsNullOrEmpty());
                RuleFor(x => x.Email)
                    .Must(x => x.IsNullOrEmpty() || UserAuthRepo.GetUserAuthByUserName(x) == null)
                    .WithErrorCode("AlreadyExists")
                    .WithMessage("Email already exists")
                    .When(x => !x.Email.IsNullOrEmpty());
            });
            RuleSet(ApplyTo.Put, () =>
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
            });
        }
    }

    [DataContract]
    [Route("/userauths/register")]
    public class Registration : IReturn<RegistrationResponse>
    {
        [DataMember(Order = 1)]
        public string UserName { get; set; }
        [DataMember(Order = 2)]
        public string FirstName { get; set; }
        [DataMember(Order = 3)]
        public string LastName { get; set; }
        [DataMember(Order = 4)]
        public string DisplayName { get; set; }
        [DataMember(Order = 5)]
        public string Email { get; set; }
        [DataMember(Order = 5)]
        public string PrimaryEmail { get; set; }
        [DataMember(Order = 6)]
        public string Password { get; set; }
        [DataMember(Order = 7)]
        public bool? AutoLogin { get; set; }
        [DataMember(Order = 8)]
        public string Continue { get; set; }
    }

    [DataContract]
    public class RegistrationResponse
    {
        public RegistrationResponse()
        {
            this.ResponseStatus = new ResponseStatus();
        }

        [DataMember(Order = 1)]
        public long UserId { get; set; }
        [DataMember(Order = 1)]
        public string UserAuthId { get; set; }
        [DataMember(Order = 2)]
        public string SessionId { get; set; }
        [DataMember(Order = 3)]
        public string UserName { get; set; }
        [DataMember(Order = 4)]
        public string ReferrerUrl { get; set; }
        [DataMember(Order = 5)]
        public ResponseStatus ResponseStatus { get; set; }
    }
    #endregion

    public class UserAuthRestService : ServiceBase
    {
        public AssignRolesService AssignRolesService { get; set; }
        public IUserAuthRepository UserAuthRepository { get; set; }
        public IUserService UserService { get; set; }

        #region Get
        public object Get(UserAuthRequest request)
        {
            if (!string.IsNullOrEmpty(request.UserAuthId))
            {
                var userAuthId = request.UserAuthId;
                var userAuth = UserAuthRepository.GetUserAuth(userAuthId).TranslateTo<UserAuthResponse>();
                userAuth.CustomId = request.UserId == 0 ? UserService.GetByUserAuthId(Convert.ToInt32(userAuthId)).Id.ToString(CultureInfo.InvariantCulture) : request.UserId.ToString(CultureInfo.InvariantCulture);
                return userAuth;
            }
            if (request.UserId != 0)
            {
                return GetUserAuth(request.UserId);
            }
            if (request.UserIds != null && request.UserIds.Length > 0)
            {
                return request.UserIds.Select(GetUserAuth).ToList();
            }
            throw new HttpError(HttpStatusCode.BadRequest, "Invalid arguments supplied.");
        }
        private UserAuthResponse GetUserAuth(long userId)
        {
            var user = UserService.Get(userId);
            var userAuth = UserAuthRepository.GetUserAuth(user.UserAuthId.ToString(CultureInfo.InvariantCulture)).TranslateTo<UserAuthResponse>();
            userAuth.CustomId = user.Id.ToString(CultureInfo.InvariantCulture);
            return userAuth;
        }
        public object Get(UserSession request)
        {
            return new UserAuthsResponse
            {
                UserSession = UserSession
            };
        }
        #endregion

        #region Put

        #endregion

        #region Post
        [Authenticate]
        public object Post(Registration request)
        {
            var reqUserAuth = request.TranslateTo<UserAuth>();
            var userAuth = UserAuthRepository.CreateUserAuth(reqUserAuth, request.Password);
            //Create new user in our Db
            var user = new User
            {
                UserAuthId = userAuth.Id
            };
            var updatedUser = UserService.CreateOrUpdate(user);
            var response = new RegistrationResponse
            {
                UserId = updatedUser.Id,
                UserAuthId = userAuth.Id.ToString(CultureInfo.InvariantCulture),
                UserName = userAuth.UserName,
                ReferrerUrl = request.Continue
            };

            var isHtml = RequestContext.ResponseContentType.MatchesContentType(ContentType.Html);
            if (isHtml)
            {
                if (string.IsNullOrEmpty(request.Continue))
                    return response;

                return new HttpResult(response)
                {
                    Location = request.Continue
                };
            }

            return response;
        }
        #endregion
    }

}
