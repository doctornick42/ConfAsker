using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Core.Interfaces
{
    public interface IQueryParser
    {
        Query ParseQuery(string queryString);
    }
}
