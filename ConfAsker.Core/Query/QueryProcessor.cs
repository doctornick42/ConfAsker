using ConfAsker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfAsker.Core;

namespace ConfAsker.Core.Query
{
    public class QueryProcessor
    {
        IQueryParser _queryParser;
        ICommandRunner _commandRunner;

        public void RunQuery(string queryString)
        {
            Query query = _queryParser.ParseQuery(queryString);
            switch (query.Command)
            {
                case ECommand.check:
                    RunCheckQuery(query);
                    break;
                case ECommand.get:
                    RunGetQuery(query);
                    break;
            }
        }

        private bool RunCheckQuery(Query query)
        {
            if (!string.IsNullOrEmpty(query.KeyValue))
            {
                return _commandRunner.IsMatchKeyValue(query.Paths,
                    query.KeyValue, query.Expected);
            }
            else if (!string.IsNullOrEmpty(query.ConnectionString))
            {
                return _commandRunner.IsMatchConnectionString(query.Paths, 
                    query.ConnectionString, query.Expected);
            }

            return false;
        }

        private void RunGetQuery(Query query)
        {
            throw new NotImplementedException();
        }
    }
}
