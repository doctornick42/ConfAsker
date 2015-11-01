using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConfAsker.Core
{
    public class ConfigReader
    {
        private Configuration _configuration;
        public ConfigReader(string configPath)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = configPath;
            _configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap,
                System.Configuration.ConfigurationUserLevel.None);
        }

        public string GetConnectionsString(string connectionStringName)
        {
            var connection = _configuration.ConnectionStrings.ConnectionStrings[connectionStringName];
            if (connection != null)
            {
                return connection.ConnectionString;
            }
            
            return null;
        }

        public string GetAppSetting(string key)
        {
            var keyValue = _configuration.AppSettings.Settings[key];
            if (keyValue != null)
            {
                return keyValue.Value;
            }
            return null;
        }
    }
}
