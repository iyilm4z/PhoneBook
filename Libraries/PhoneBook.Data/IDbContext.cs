using System.Data.Entity;
using PhoneBook.Core.Data;

namespace PhoneBook.Data
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        int SaveChanges();
    }
}