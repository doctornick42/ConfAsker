using ConfAsker.Core.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Core.Interfaces
{
    public interface ICommandRunner
    {
        OperationResult IsMatchKeyValue(List<string> paths, string key, string expectedValue);
        OperationResult IsMatchConnectionString(List<string> paths, string key, string expectedValue);
    }
}
