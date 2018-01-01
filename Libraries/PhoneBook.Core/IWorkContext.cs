using PhoneBook.Core.Domain;

namespace PhoneBook.Core
{
    public interface IWorkContext
    {
        User CurrentUser { get; }

        bool IsAdmin { get; }
    }
}