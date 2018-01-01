using System.Configuration;
using System.Runtime.CompilerServices;
using PhoneBook.Core.Infrastructure;

namespace PhoneBook.Core.Engine
{
    public class EngineContext
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize()
        {
            if (Singleton<IEngine>.Instance != null)
                return Singleton<IEngine>.Instance;

            Singleton<IEngine>.Instance = new PhoneBookEngine();
            var config = ConfigurationManager.GetSection("PhoneBookConfig") as PhoneBookConfig;
            Singleton<IEngine>.Instance.Initialize(config);

            return Singleton<IEngine>.Instance;
        }

        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                    Initialize();

                return Singleton<IEngine>.Instance;
            }
        }
    }
}