using FileManagerOnline.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerOnline.Core
{
    public class CommandRunner : ICommandRunner
    {
        public bool IsMatchKeyValue(string path, string key, string expectedValue)
        {
            return IsMatch(path, key, expectedValue, x => x.GetAppSetting(key));
        }

        public bool IsMatchConnectionString(string path, string key, string expectedValue)
        {
            return IsMatch(path, key, expectedValue, x => x.GetConnectionsString(key));
        }

        private bool IsMatch(string path, string key, string expectedValue,
            Func<ConfigReader, string> chosenFunction)
        {
            bool result = false;

            ConfigReader configReader = new ConfigReader(path);
            string foundValue = chosenFunction(configReader);

            if (!String.IsNullOrEmpty(foundValue) && foundValue == expectedValue)
            {
                result = true;
            }

            return result;
        }
    }
}
