using System.Configuration;
using System.Web.Mvc;
using iShare.BusinessLogic;
using iShare.BusinessLogic.Contracts;
using iShare.DataContext.OrmLiteRepositories;
using iShare.DataContext.Repositories;
using iShare.DataInterface;
using iShare.Web.App_Start;
using iShare.Web.Models;
using iShare.Web.RestServices;
using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using ServiceStack.Logging.Elmah;
using ServiceStack.Logging.Support.Logging;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using IDbConnectionFactory = ServiceStack.OrmLite.IDbConnectionFactory;

[assembly: WebActivator.PreApplicationStartMethod(typeof(AppHost), "Start")]
/**
 * Entire ServiceStack Starter Template configured with a 'Hello' Web Service and a 'Todo' Rest Service.
 *
 * Auto-Generated Metadata API page at: /metadata
 * See other complete web service examples at: https://github.com/ServiceStack/ServiceStack.Examples
 */

namespace iShare.Web.App_Start
{
	//A customizeable typed UserSession that can be extended with your own properties
	//To access ServiceStack's Session, Cache, etc from MVC Controllers inherit from ControllerBase<CustomUserSession>
    //public class CustomUserSession : AuthUserSession
    //{
    //    public string CustomProperty { get; set; }
    //}

	public class AppHost : AppHostBase
	{		
		public AppHost() //Tell ServiceStack the name and where to find your web services
			: base("iShare Rest Service", typeof(CharityRestService).Assembly) { }

		public override void Configure(Container container)
		{
			//Set JSON web services to return idiomatic JSON camelCase properties
			ServiceStack.Text.JsConfig.EmitCamelCaseNames = false;
		
			//Uncomment to change the default ServiceStack configuration
			//SetConfig(new EndpointHostConfig {
			//});

            //Use Elmah with ServiceStack
            LogManager.LogFactory = new ElmahLogFactory(new NullLogFactory());

            //Make the default lifetime of objects limited to request
            container.DefaultReuse = ReuseScope.Request;

            //Uncomment to use Entity Framework
            //RegisterEfServicesAndRepositories(container);
		    RegisterOrmLiteServicesAndRepositories(container);
            RegisterServices(container);
		    RegisterCacheAndStorage(container);
            
            //Enable Authentication
			ConfigureAuth(container);

			//Set MVC to use the same Funq IOC as ServiceStack
			ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
		}

	    private void RegisterOrmLiteServicesAndRepositories(Container container)
	    {
            var connectionString = ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
            //repositories
            container.Register<IUserRepository>(c => new UserOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<ICharityRepository>(c => new CharityOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<ICauseRepository>(c => new CauseOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<ICategoryRepository>(c => new CategoryOrmLiteRepository(c.Resolve<IDbConnectionFactory>()));
            //database
            OrmLiteConfigure.Initialize(container, connectionString);
	        
	    }
        private void RegisterServices(Container container)
        {
            //services
            container.Register<IUserService>(c => new UserService(c.Resolve<IUserRepository>()));
            container.Register<ICharityService>(c => new CharityService(c.Resolve<ICharityRepository>()));
            container.Register<ICauseService>(c => new CauseService(c.Resolve<ICauseRepository>()));
            container.Register<ICategoryService>(c => new CategoryService(c.Resolve<ICategoryRepository>()));
            
        }
	    private void RegisterEfServicesAndRepositories(Container container)
	    {
            //Make the default lifetime of objects limited to request
            var connectionString = ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(connectionString, SqlServerOrmLiteDialectProvider.Instance)
                {
                    ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
                });

            //---Entity Framework (Uncomment to use)
            //database
            EfConfigure.Initialize(connectionString);
            container.Register<IUnitOfWork>(c => new DataContext.DataContext());
            //repositories
            container.Register<IUserRepository>(c => new UserRepository(c.Resolve<IUnitOfWork>()));
            container.Register<ICauseRepository>(c => new CauseRepository(c.Resolve<IUnitOfWork>()));
            container.Register<ICharityRepository>(c => new CharityRepository(c.Resolve<IUnitOfWork>()));
            container.Register<ICategoryRepository>(c => new CategoryRepository(c.Resolve<IUnitOfWork>()));
        }

        // Uncomment to enable ServiceStack Authentication and CustomUserSession
        private void ConfigureAuth(Funq.Container container)
        {
            var appSettings = new AppSettings();

            //Default route: /auth/{provider}
            Plugins.Add(new AuthFeature(() => new CustomUserSession(container.Resolve<IUserService>()),
                new IAuthProvider[] {
					new CredentialsAuthProvider(appSettings), 
				}) { HtmlRedirect = null });

            //Default route: /register
            Plugins.Add(new RegistrationFeature()); 

            //Requires ConnectionString configured in Web.Config
            var connectionString = ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider));

            //container.Register<IUserAuthRepository>(c =>
            //    new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<IUserAuthRepository>(c => new CustomOrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

            var authRepo = (OrmLiteAuthRepository)container.Resolve<IUserAuthRepository>();
            authRepo.CreateMissingTables();
        }
        
        private void RegisterCacheAndStorage(Container container)
        {
            container.Register<ICacheClient>(c => new MemoryCacheClient()).ReusedWithin(ReuseScope.Container);
            container.Register<ISessionFactory>(c => new SessionFactory(c.Resolve<ICacheClient>())).ReusedWithin(ReuseScope.Container);
        }

		public static void Start()
		{
			new AppHost().Init();
		}
	}
}