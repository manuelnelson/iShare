using ServiceStack.ServiceInterface;
using iShare.Web.Models;

namespace iShare.Web.RestServices
{
    public class ServiceBase : Service
    {
        public CustomUserSession UserSession
        {
            get { return SessionAs<CustomUserSession>(); }
        }
    }
}