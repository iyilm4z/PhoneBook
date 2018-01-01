using Autofac;
using PhoneBook.Core.Infrastructure;
using PhoneBook.Core.Reflection;

namespace PhoneBook.Core.DependencyInjection
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, PhoneBookConfig config);

        int Order { get; }
    }
}