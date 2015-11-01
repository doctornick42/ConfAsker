using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerOnline.Core.Interfaces
{
    public interface ICommandRunner
    {
        bool IsMatchKeyValue(List<string> paths, string key, string expectedValue);
        bool IsMatchConnectionString(List<string> paths, string key, string expectedValue);
    }
}
