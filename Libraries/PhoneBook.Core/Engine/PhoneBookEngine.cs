#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using PhoneBook.Core.DependencyInjection;
using PhoneBook.Core.Infrastructure;
using PhoneBook.Core.Reflection;

#endregion

namespace PhoneBook.Core.Engine
{
    public class PhoneBookEngine : IEngine
    {
        #region Utils

        private void RunStartupTasks()
        {
            var typeFinder = IoCManager.Resolve<ITypeFinder>();

            var startUpTaskInstances = typeFinder.FindClassesOfType<IStartupTask>()
                .Select(drType => (IStartupTask)Activator.CreateInstance(drType))
                .OrderBy(t => t.Order)
                .ToList();
            foreach (var startUpTask in startUpTaskInstances)
                startUpTask.Execute();
        }


        private void RegisterDependencies(PhoneBookConfig config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(config).As<PhoneBookConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            var typeFinder = new TypeFinder();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            var drInstances = typeFinder.FindClassesOfType<IDependencyRegistrar>()
                .Select(drType => (IDependencyRegistrar)Activator.CreateInstance(drType))
                .OrderBy(t => t.Order)
                .ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder, config);

            var container = builder.Build();
            IoCManager = new IoCManager(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        #endregion

        #region Methods

        public void Initialize(PhoneBookConfig config)
        {
            RegisterDependencies(config);
            RunStartupTasks();
        }


        #endregion

        #region Props

        public IoCManager IoCManager { get; private set; }

        #endregion
    }
}