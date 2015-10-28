using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerOnline.Core.Interfaces
{
    public interface ICommandRunner
    {
        bool IsMatchKeyValue(string path, string key, string expectedValue);
        bool IsMatchConnectionString(string path, string key, string expectedValue);
    }
}
