using ConfAsker.Core.QueryProcessing;
using ConfAsker.Core.Units;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Tests
{
    [TestFixture]
    public class QueryValidatorTests
    {
        private readonly List<string> _invalidQueries = new List<string>() 
        {
            "ch4eck keyValue:'TestKey' expected:'TestValue'",
            "check keyValue:TestKey expected:'TestValue'",
            "check myStrangeKey:'123456' keyValue:'TestKey'",
            "check keyValue:TestKey"
        };

        private readonly List<string> _validQueries = new List<string>() 
        {
            "check keyValue:'TestKey' expected:'TestValue'",
            @"check keyValue:'TestKey' expected:'TestValue' paths:'D:\something\web.config'",
            "check connectionString:'TestCS' expected:'myConnectionString'",
            "get keyValue:'TestKey'",
            @"check paths:'\qwerty\something.config' keyValue:'TestKey' expected:'TestValue'"
        };

        [Test]
        public void TestQuerySyntaxValidator()
        {
            QueryValidator queryValidator = new QueryValidator();

            OperationResult validQueriesValidationResult = new OperationResult();
            foreach (var query in _validQueries)
            {
                OperationResult valResult = queryValidator.ValidateQuerySyntax(query);

                validQueriesValidationResult.Successful &= valResult.Successful;
                if (!valResult.Successful)
                {
                    validQueriesValidationResult.Description +=
                        String.Format("Error in query '{0}': {1}. ",
                            query, valResult.Description);
                }
            }

            Assert.IsTrue(validQueriesValidationResult.Successful,
                validQueriesValidationResult.Description);

            OperationResult invalidQueriesValidationResult = new OperationResult();
            foreach (var query in _invalidQueries)
            {
                OperationResult valResult = queryValidator.ValidateQuerySyntax(query);

                invalidQueriesValidationResult.Successful &= valResult.Successful;
                if (!valResult.Successful)
                {
                    invalidQueriesValidationResult.Description +=
                        String.Format("Error in query '{0}': {1} .",
                            query, valResult.Description);
                }
            }

            Assert.IsFalse(invalidQueriesValidationResult.Successful,
                invalidQueriesValidationResult.Description);
        }
    }
}
