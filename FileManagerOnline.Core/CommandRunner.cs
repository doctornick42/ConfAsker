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
            List<string> paths = new List<string>() { path };
            return IsMatchKeyValue(path, key, expectedValue);
        }

        public bool IsMatchConnectionString(string path, string key, string expectedValue)
        {
            List<string> paths = new List<string>() { path };
            return IsMatchConnectionString(path, key, expectedValue);
        }

        public bool IsMatchKeyValue(List<string> paths, string key, string expectedValue)
        {
            return IsMatch(paths, key, expectedValue, x => x.GetAppSetting(key));
        }

        public bool IsMatchConnectionString(List<string> paths, string key, string expectedValue)
        {
            return IsMatch(paths, key, expectedValue, x => x.GetConnectionsString(key));
        }

        private bool IsMatch(List<string> paths, string key, string expectedValue,
            Func<ConfigReader, string> chosenFunction)
        {
            bool result = true;

            foreach (string path in paths)
            {
                ConfigReader configReader = new ConfigReader(path);
                string foundValue = chosenFunction(configReader);

                if (String.IsNullOrEmpty(foundValue) || foundValue != expectedValue)
                {
                    result &= false;
                }
            }

            return result;
        }
    }
}
