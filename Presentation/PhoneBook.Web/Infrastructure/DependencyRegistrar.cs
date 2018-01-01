using Autofac;
using PhoneBook.Core.DependencyInjection;
using PhoneBook.Core.Infrastructure;
using PhoneBook.Core.Reflection;
using PhoneBook.Web.Factories;

namespace PhoneBook.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, PhoneBookConfig config)
        {
            //factories
            builder.RegisterType<UserModelFactory>().As<IUserModelFactory>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}