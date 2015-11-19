using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Core.Interfaces
{
    public interface IDirectoryInfoProcessor
    {
        List<string> GetQueryPathes(IEnumerable<string> incommingStrings);

        List<string> GetDirectories(string path);

        List<string> GetFiles(string path);
    }
}
