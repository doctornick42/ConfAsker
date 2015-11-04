using ConfAsker.Core.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConfAsker.Core.Query
{
    public class QueryValidator
    {
        private readonly List<string> _availableArgumentsKeys = new List<string>() 
        {
            "keyValue",
            "connectionString",
            "expected",
            "caseInsensitive"
        };

        private const string _argumentFormat = @"^'.*'$";

        public OperationResult ValidateQuerySyntax(string query)
        {
            string[] stringArray = query.Split(' ');
            return ValidateQuerySyntax(stringArray);
        }

        public OperationResult ValidateQuerySyntax(string[] query)
        {
            OperationResult result = new OperationResult();
            if (query.Length == 0) 
            {
                result.Successful = false;
                result.Description = "Query is empty.";
                return result;
            }

            string firstWord = query[0];
            ECommand command;
            if (!Enum.TryParse(firstWord, out command))
            {
                result.Successful = false;
                result.Description = "Command can't be recognized.";
                return result;
            }

            string[] argumentsArray = new string[query.Length - 1];
            Array.Copy(query, 1, argumentsArray, 0, query.Length - 1);
            if (argumentsArray.Length == 0)
            {
                result.Successful = false;
                result.Description = "Arguments list is empty.";
                return result;
            }

            OperationResult argumentsValidationResult = ValidateArguments(command, argumentsArray);
            if (!argumentsValidationResult.Successful)
            {
                result = argumentsValidationResult;
            }

            
            return result;
        }

        public OperationResult ValidateQuery(Query query)
        {
            throw new NotImplementedException();
        }

        private OperationResult ValidateArguments(ECommand command, string[] arguments)
        {
            OperationResult result = new OperationResult();
            Dictionary<string, string> argumentsDict;
            try
            {
                argumentsDict = (from arg in arguments
                                 let splittedArg = arg.Split(':')
                                 select splittedArg)
                                 .ToDictionary(x => x[0], x => x[1]);
            }
            catch (IndexOutOfRangeException)
            {
                result.Successful = false;
                result.Description = "Some argument has wrong format.";
                return result;
            }

            var unknownArguments = argumentsDict
                .Keys
                .Where(x => !_availableArgumentsKeys.Contains(x));
            
            if (unknownArguments.Any())
            {
                result.Description = String.Format("Unknown arguments: {0}", 
                    String.Join(", ", unknownArguments));
                result.Successful = false;
                return result;
            }

            Regex singleQuotesRegex = new Regex(_argumentFormat);

            //caseInsensitive argument is the only argument that
            //could exists without single quotes around it
            var notSingleQuotedArgs = argumentsDict
                .Values
                .Where(x => x != "caseInsensitive" && !singleQuotesRegex.IsMatch(x));

            if (notSingleQuotedArgs.Any(x => x != "caseInsensitive"))
            {
                result.Description = String.Format("Arguments {0} must be inside single quotes",
                    String.Join(", ", notSingleQuotedArgs));
                result.Successful = false;
                return result;
            }

            if (command == ECommand.check && !argumentsDict.ContainsKey("expected"))
            {
                result.Description = "Check query must contains 'expected'";
                result.Successful = false;
                return result;
            }

            return result;
        }
    }
}
