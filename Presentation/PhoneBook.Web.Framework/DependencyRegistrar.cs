using System.Linq;
using Autofac;
using Autofac.Integration.Mvc;
using PhoneBook.Core;
using PhoneBook.Core.Data;
using PhoneBook.Core.DependencyInjection;
using PhoneBook.Core.Infrastructure;
using PhoneBook.Core.Reflection;
using PhoneBook.Data;
using PhoneBook.Services.Authentication;
using PhoneBook.Services.Departments;
using PhoneBook.Services.Users;

namespace PhoneBook.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, PhoneBookConfig config)
        {
            // http
            builder.RegisterModule(new AutofacWebTypesModule());

            // controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //data layer
            builder.Register<IDbContext>(c => new EfDbContext(config.DataConnectionString)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            //services
            builder.RegisterType<DepartmentService>().As<IDepartmentService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
        }

        public int Order => 0;
    }
}
