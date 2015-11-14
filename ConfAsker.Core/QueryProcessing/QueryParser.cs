using ConfAsker.Core;
using ConfAsker.Core.FileSystem;
using ConfAsker.Core.Interfaces;
using ConfAsker.Core.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Core.QueryProcessing
{
    public class QueryParser : IQueryParser
    {
        private IQueryValidator _queryValidator;
        private IDirectoryInfoProcessor _directoryInfoProcessor;

        public QueryParser(IQueryValidator queryValidator,
            IDirectoryInfoProcessor directoryInfoProcessor)
        {
            _queryValidator = queryValidator;
            _directoryInfoProcessor = directoryInfoProcessor;
        }

        public Query ParseQuery(string queryString)
        {
            OperationResult validationResult = _queryValidator
                .ValidateQuerySyntax(queryString);

            if (!validationResult.Successful)
            {
                throw new ValidationException(validationResult.Description);
            }

            string[] splitedString = queryString.Split(' ');
            return ParseQuery(splitedString);
        }

        public Query ParseQuery(string[] queryStringArray)
        {
            OperationResult validationResult = _queryValidator
                .ValidateQuerySyntax(queryStringArray);

            if (!validationResult.Successful)
            {
                throw new ValidationException(validationResult.Description);
            }

            ECommand command = (ECommand)Enum.Parse(typeof(ECommand), queryStringArray[0]);

            string[] arguments = new string[queryStringArray.Length - 1];
            Array.Copy(queryStringArray, 1, arguments, 0, queryStringArray.Length - 1);

            string[] argumentKeyValuDelimiter = new string[1] { ":\"" };

            Dictionary<string, string> argumentsDict = (from arg in arguments
                                                        let splittedArg = arg.Split(new char[] { ':' }, 2)
                                                        select splittedArg)
                                 .ToDictionary(x => x[0], x => x[1]);

            Query query = new Query();
            query.Command = command;
            query.ConnectionString = GetArgumentFromDictionary(argumentsDict, "connectionString");
            query.Expected = GetArgumentFromDictionary(argumentsDict, "expected");
            query.KeyValue = GetArgumentFromDictionary(argumentsDict, "keyValue");

            string unparsedPathsString = GetArgumentFromDictionary(argumentsDict, "paths");
            List<string> originalPaths = String.IsNullOrWhiteSpace(unparsedPathsString)
                ? new List<string>() { Directory.GetCurrentDirectory() }
                : unparsedPathsString.Split(',').ToList();

            query.Paths = _directoryInfoProcessor.GetQueryPathes(originalPaths);

            return query;
        }
        
        private string GetArgumentFromDictionary(Dictionary<string, string> dict, string searchingKey)
        {
            return dict.ContainsKey(searchingKey)
                ? dict[searchingKey].Trim('\'')
                : null;
        }
    }
}
