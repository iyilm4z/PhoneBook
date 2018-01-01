#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

#endregion

namespace PhoneBook.Core.Reflection
{
    public class TypeFinder : ITypeFinder
    {
        #region Props

        private string AssemblySkipPattern { get; set; } =
            "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|" +
            "^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|" +
            "^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|" +
            "^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|" +
            "^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|" +
            "^Remotion|^RestSharp|^Rhino|^Telerik|^Iesi|^TestDriven|^TestFu|" +
            "^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease";

        #endregion

        #region Utils

        private IList<Assembly> GetAssembliesInAppDomain()
        {
            var assemblies = new List<Assembly>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                if (Matches(assembly.FullName))
                    if (assemblies.All(x => x.FullName != assembly.FullName))
                        assemblies.Add(assembly);

            return assemblies;
        }

        private bool Matches(string assemblyFullName) =>
            !Matches(assemblyFullName, AssemblySkipPattern);

        private bool Matches(string assemblyFullName, string pattern) =>
            Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            try
            {
                var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
                return (from implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null)
                        where implementedInterface.IsGenericType
                        select genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition()))
                        .FirstOrDefault();
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Methods

        public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true) =>
            FindClassesOfType(typeof(T), onlyConcreteClasses);

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true) =>
            FindClassesOfType(assignTypeFrom, GetAssemblies(), onlyConcreteClasses);

        public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true) =>
            FindClassesOfType(typeof(T), assemblies, onlyConcreteClasses);

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            var result = new List<Type>();
            try
            {
                foreach (var assembly in assemblies)
                {
                    Type[] types = null;
                    try
                    {
                        types = assembly.GetTypes();
                    }
                    catch
                    {
                        // ignored
                    }

                    if (types == null)
                        continue;

                    foreach (var type in types)
                        if (assignTypeFrom.IsAssignableFrom(type) ||
                            assignTypeFrom.IsGenericTypeDefinition &&
                            DoesTypeImplementOpenGeneric(type, assignTypeFrom))
                            if (!type.IsInterface)
                                if (onlyConcreteClasses)
                                {
                                    if (type.IsClass && !type.IsAbstract)
                                        result.Add(type);
                                }
                                else
                                    result.Add(type);
                }
            }
            catch
            {
                // ignored
            }
            return result;
        }

        public IList<Assembly> GetAssemblies() => GetAssembliesInAppDomain();

        #endregion     
    }
}