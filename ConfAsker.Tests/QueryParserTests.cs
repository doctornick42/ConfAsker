using ConfAsker.Core.Interfaces;
using ConfAsker.Core.QueryProcessing;
using ConfAsker.Core.Units;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Tests
{
    [TestFixture]
    public class QueryParserTests
    {
        [Test]
        public void TestValidationCalling()
        {
            IQueryValidator queryValidator = MockRepository.GenerateStub<IQueryValidator>();
            string query = "check keyValue:'myKey' expected:'myValue'";

            queryValidator.Stub(x => x.ValidateQuerySyntax(Arg<string[]>.Is.Anything))
                .Return(new OperationResult(successful: true));
            queryValidator.Stub(x => x.ValidateQuerySyntax(Arg<string>.Is.Anything))
                .Return(new OperationResult(successful: true));

            QueryParser parser = new QueryParser(queryValidator);
            parser.ParseQuery(query);

            queryValidator.AssertWasCalled(x => x.ValidateQuerySyntax(query));
        }

        [Test]
        public void TestQueryParsing()
        {
            IQueryValidator queryValidator = MockRepository.GenerateStub<IQueryValidator>();
            string testQueryString = 
                @"check keyValue:'someKey' paths:'\web.config' expected:'someValue'";

            queryValidator.Stub(x => x.ValidateQuerySyntax(Arg<string[]>.Is.Anything))
                .Return(new OperationResult(successful: true));
            queryValidator.Stub(x => x.ValidateQuerySyntax(Arg<string>.Is.Anything))
                .Return(new OperationResult(successful: true));


            QueryParser parser = new QueryParser(queryValidator);
            Query query = parser.ParseQuery(testQueryString);

            Assert.AreEqual(ECommand.check, query.Command);
            Assert.AreEqual(new List<string>() { @"\web.config" }, query.Paths);
            Assert.AreEqual("someKey", query.KeyValue);
            Assert.AreEqual("someValue", query.Expected);
        }
    }
}
