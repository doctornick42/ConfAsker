using FileManagerOnline.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerOnline.Core
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

        private void RunCheckQuery(Query query)
        {
            if (!string.IsNullOrEmpty(query.KeyValue))
            {
                CheckKeyValue(query.Paths, query.KeyValue, query.Expected, query.Section);
            }
            else if (!string.IsNullOrEmpty(query.ConnectionString))
            {
                CheckConnectionString(query.Paths, query.ConnectionString, query.Expected);
            }
        }

        private void CheckKeyValue(List<string> paths, string key, 
            string expected, string section = null)
        {
            _commandRunner.IsMatchKeyValue(paths, key, expected);
        }

        private void CheckConnectionString(List<string> paths, string name, string expected)
        {
            _commandRunner.IsMatchConnectionString(paths, name, expected);
        }

        private void RunGetQuery(Query query)
        {
            throw new NotImplementedException();
        }
    }
}
