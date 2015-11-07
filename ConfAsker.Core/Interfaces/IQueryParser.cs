using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Core.Interfaces
{
    public interface IQueryParser
    {
        ConfAsker.Core.Query.Query ParseQuery(string queryString);

        /// <summary>
        /// Convert query string to an instance of a Query class.
        /// </summary>
        /// <param name="queryStringArray">
        /// Query string that presented by arguments array from ConsoleApplication
        /// </param>
        ConfAsker.Core.Query.Query ParseQuery(string[] queryStringArray);
    }
}
