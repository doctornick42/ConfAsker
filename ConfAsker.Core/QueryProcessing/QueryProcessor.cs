using ConfAsker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfAsker.Core;
using ConfAsker.Core.Units;

namespace ConfAsker.Core.QueryProcessing
{
    public class QueryProcessor
    {
        IQueryParser _queryParser;
        ICommandRunner _commandRunner;

        public QueryProcessor(ICommandRunner commandRunner, IQueryParser queryParser)
        {
            _commandRunner = commandRunner;
            _queryParser = queryParser;
        }

        public OperationResult RunQuery(string queryString)
        {
            OperationResult result = new OperationResult(false);
            Query query = _queryParser.ParseQuery(queryString);

            switch (query.Command)
            {
                case ECommand.check:
                    result = RunCheckQuery(query);
                    break;
                case ECommand.get:
                    result = RunGetQuery(query);
                    break;
            }

            return result;
        }

        public OperationResult RunQuery(string[] queryStringArray)
        {
            OperationResult result = new OperationResult(false);
            Query query = _queryParser.ParseQuery(queryStringArray);
            switch (query.Command)
            {
                case ECommand.check:
                    result = RunCheckQuery(query);
                    break;
                case ECommand.get:
                    result = RunGetQuery(query);
                    break;
            }

            return result;
        }

        private OperationResult RunCheckQuery(Query query)
        {
            OperationResult result = new OperationResult(false);

            if (!string.IsNullOrEmpty(query.KeyValue))
            {
                result = _commandRunner.IsMatchKeyValue(query.Paths,
                    query.KeyValue, query.Expected);
            }
            else if (!string.IsNullOrEmpty(query.ConnectionString))
            {
                result = _commandRunner.IsMatchConnectionString(query.Paths, 
                    query.ConnectionString, query.Expected);
            }

            return result;
        }

        private OperationResult RunGetQuery(Query query)
        {
            throw new NotImplementedException();
        }
    }
}
