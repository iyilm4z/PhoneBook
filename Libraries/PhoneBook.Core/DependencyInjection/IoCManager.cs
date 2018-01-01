#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;

#endregion

namespace PhoneBook.Core.DependencyInjection
{
    public class IoCManager
    {
        #region Ctors

        public IoCManager(IContainer container)
        {
            Container = container;
        }

        #endregion

        #region Utils

        private ILifetimeScope Scope()
        {
            try
            {
                return HttpContext.Current != null ?
                    AutofacDependencyResolver.Current.RequestLifetimeScope :
                    Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
            catch
            {
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
        }

        #endregion

        #region Methods

        public object Resolve(Type type, ILifetimeScope scope = null)
        {
            if (scope == null)
                scope = Scope();

            return scope.Resolve(type);
        }

        public T Resolve<T>(string key = "", ILifetimeScope scope = null) where T : class
        {
            if (scope == null)
                scope = Scope();

            return string.IsNullOrEmpty(key) ? scope.Resolve<T>() : scope.ResolveKeyed<T>(key);
        }

        public List<T> ResolveAll<T>(string key = "", ILifetimeScope scope = null)
        {
            if (scope == null)
                scope = Scope();

            return string.IsNullOrEmpty(key) ?
                scope.Resolve<IEnumerable<T>>().ToList() :
                scope.ResolveKeyed<IEnumerable<T>>(key).ToList();
        }

        #endregion

        #region Props

        public IContainer Container { get; }

        #endregion
    }
}