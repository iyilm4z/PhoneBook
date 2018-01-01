#region Usings

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using PhoneBook.Core.Data;
using PhoneBook.Core.Infrastructure;

#endregion

namespace PhoneBook.Data
{
    public class EfDbContext : DbContext, IDbContext
    {
        #region Ctors

        public EfDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        #endregion

        #region Utils

        private void AddAllConfigurations(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }

        private string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        #endregion

        #region Overriding

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            AddAllConfigurations(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region Methods

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity => base.Set<TEntity>();

        #endregion
    }
}