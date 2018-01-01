using System.Configuration;
using System.Xml;

namespace PhoneBook.Core.Infrastructure
{
    public class PhoneBookConfig : IConfigurationSectionHandler
    {
        public string DataConnectionString { get; set; }

        public object Create(object parent, object configContext, XmlNode section)
        {
            var config = new PhoneBookConfig();

            var redisCachingNode = section.SelectSingleNode("Database");
            var connectionStringAttribute = redisCachingNode?.Attributes?["ConnectionString"];
            if (connectionStringAttribute != null)
                config.DataConnectionString = connectionStringAttribute.Value;

            return config;
        }
    }
}