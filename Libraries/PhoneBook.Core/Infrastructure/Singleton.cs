using System;
using System.Collections.Generic;

namespace PhoneBook.Core.Infrastructure
{
    public class Singleton<T>
    {
        private static T _instance;

        static Singleton()
        {
            AllSingletons = new Dictionary<Type, object>();
        }

        public static T Instance
        {
            get => _instance;
            set
            {
                _instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }

        private static IDictionary<Type, object> AllSingletons { get; }
    }
}