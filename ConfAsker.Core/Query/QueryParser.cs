using ConfAsker.Core;
using ConfAsker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Core.Query
{
    public class QueryParser : IQueryParser
    {
        public Query ParseQuery(string queryString)
        {
            string[] splitedString = queryString.Split(' ');
            return ParseQuery(splitedString);
        }


        public Query ParseQuery(string[] queryStringArray)
        {
            throw new NotImplementedException();
        }
    }
}
