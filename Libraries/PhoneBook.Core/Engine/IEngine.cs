using PhoneBook.Core.DependencyInjection;
using PhoneBook.Core.Infrastructure;

namespace PhoneBook.Core.Engine
{
    public interface IEngine
    {
        void Initialize(PhoneBookConfig config);

        IoCManager IoCManager { get; }
    }
}