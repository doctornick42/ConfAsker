using ConfAsker.Core.Interfaces;
using ConfAsker.Core.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConfAsker.Core.QueryProcessing
{
    public class QueryValidator : IQueryValidator
    {
        private readonly List<string> _availableArgumentsKeys = new List<string>() 
        {
            "keyValue",
            "connectionString",
            "expected",
            "caseInsensitive",
            "paths"
        };

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

        private OperationResult ValidateArguments(ECommand command, string[] arguments)
        {
            OperationResult result = new OperationResult();
            Dictionary<string, string> argumentsDict;
            try
            {
                argumentsDict = (from arg in arguments
                                 let splittedArg = arg.Split(new char[] { ':' }, 2)
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
