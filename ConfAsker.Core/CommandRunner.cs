using ConfAsker.Core.FileSystem;
using ConfAsker.Core.Interfaces;
using ConfAsker.Core.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Core
{
    public class CommandRunner : ICommandRunner
    {
        public OperationResult IsMatchKeyValue(string path, string key, string expectedValue)
        {
            List<string> paths = new List<string>() { path };
            return IsMatchKeyValue(path, key, expectedValue);
        }

        public OperationResult IsMatchConnectionString(string path, string key, 
            string expectedValue)
        {
            List<string> paths = new List<string>() { path };
            return IsMatchConnectionString(path, key, expectedValue);
        }

        public OperationResult IsMatchKeyValue(List<string> paths, string key, 
            string expectedValue)
        {
            return IsMatch(paths, key, expectedValue, x => x.GetAppSetting(key));
        }

        public OperationResult IsMatchConnectionString(List<string> paths, 
            string key, string expectedValue)
        {
            return IsMatch(paths, key, expectedValue, x => x.GetConnectionsString(key));
        }

        private OperationResult IsMatch(List<string> paths, string key, string expectedValue,
            Func<ConfigReader, string> chosenFunction)
        {
            OperationResult result = new OperationResult();

            foreach (string path in paths)
            {
                ConfigReader configReader = new ConfigReader(path);
                string foundValue = chosenFunction(configReader);

                if (String.IsNullOrEmpty(foundValue) || foundValue != expectedValue)
                {
                    result.Successful &= false;
                    result.Description += String.Format("Mismatch in file '{0}'. ", path); 
                }
            }

            return result;
        }
    }
}
