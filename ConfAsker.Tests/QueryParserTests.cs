using ConfAsker.Core.Interfaces;
using ConfAsker.Core.QueryProcessing;
using ConfAsker.Core.Units;
using ConfAsker.Tests.Helpers;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Tests
{
    [TestFixture]
    public class QueryParserTests
    {
        private const string _tempDirPath = "tempForTest";
        private DirectoryInfo _tempDir;
        private const string _mainTestConfigFilename = @"\mainTestConfig.config";

        [SetUp]
        public void SetUp()
        {
            var fileSystemHelper = new FileSystemTestsHelper(_tempDirPath,
                _mainTestConfigFilename);
            _tempDir = fileSystemHelper.CreateOrClearTempFolder();
            fileSystemHelper.FillTempFolder();
        }

        [Test]
        public void TestValidationCalling()
        {
            IQueryValidator queryValidator = MockRepository.GenerateStub<IQueryValidator>();
            IDirectoryInfoProcessor directoryInfoProcessor =
                MockRepository.GenerateStub<IDirectoryInfoProcessor>();

            string query = "check keyValue:\"myKey\" expected:\"myValue\"";

            queryValidator.Stub(x => x.ValidateQuerySyntax(Arg<string[]>.Is.Anything))
                .Return(new OperationResult(successful: true));
            queryValidator.Stub(x => x.ValidateQuerySyntax(Arg<string>.Is.Anything))
                .Return(new OperationResult(successful: true));

            directoryInfoProcessor.Stub(x => x.GetQueryPathes(Arg<string[]>.Is.Anything))
                .Return(new List<string>());

            QueryParser parser = new QueryParser(queryValidator, directoryInfoProcessor);
            parser.ParseQuery(query);

            queryValidator.AssertWasCalled(x => x.ValidateQuerySyntax(query));
        }

        [Test]
        public void TestQueryParsing()
        {
            IQueryValidator queryValidator = MockRepository.GenerateStub<IQueryValidator>();
            IDirectoryInfoProcessor directoryInfoProcessor =
                MockRepository.GenerateStub<IDirectoryInfoProcessor>();

            string testQueryString = 
                "check keyValue:\"someKey\" paths:\"web.config\" expected:\"someValue\"";

            queryValidator.Stub(x => x.ValidateQuerySyntax(Arg<string[]>.Is.Anything))
                .Return(new OperationResult(successful: true));
            queryValidator.Stub(x => x.ValidateQuerySyntax(Arg<string>.Is.Anything))
                .Return(new OperationResult(successful: true));

            directoryInfoProcessor.Stub(x => x.GetQueryPathes(Arg<string[]>.Is.Anything))
                .Return(new List<string>() { "web.config" });

            QueryParser parser = new QueryParser(queryValidator, directoryInfoProcessor);
            Query query = parser.ParseQuery(testQueryString);

            Assert.AreEqual(ECommand.check, query.Command);
            Assert.AreEqual(new List<string>() { "web.config" }, query.Paths);
            Assert.AreEqual("someKey", query.KeyValue);
            Assert.AreEqual("someValue", query.Expected);
        }
    }
}
