using System;
using NUnit.Framework;
using ConfAsker.Core;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using ConfAsker.Core.FileSystem;
using ConfAsker.Core.QueryProcessing;
using ConfAsker.Tests.Helpers;

namespace ConfAsker.Tests
{
    [TestFixture]
    public class UnitTests
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
        public void TestConfigReader()
        {
            var configReader = new ConfigReader(_tempDir + _mainTestConfigFilename);

            var secondValue = configReader.GetAppSetting("secondKey");
            Assert.AreEqual("secondValue", secondValue);
        }

        [Test]
        public void TestGetDirectoriesAndFilesList()
        {
            DirectoryInfoProcessor dirInfoProcessor = new DirectoryInfoProcessor();
            var directories = dirInfoProcessor.GetDirectories(_tempDir.FullName);
            var files = dirInfoProcessor.GetFiles(_tempDir.FullName);

            var expectedOrderedDirectories = _tempDir.GetDirectories()
                .Select(x => x.Name)
                .OrderBy(x => x);

            var expectedOrderedFiles = _tempDir.GetFiles()
                .Select(x => x.Name)
                .OrderBy(x => x);

            Assert.AreEqual(expectedOrderedDirectories,
                directories.ToList().OrderBy(x => x));
            Assert.AreEqual(expectedOrderedFiles,
                files.ToList().OrderBy(x => x));

        }

        [Test]
        public void TestGetQueryPathes()
        {
            DirectoryInfoProcessor dirInfoProcessor =
                new DirectoryInfoProcessor();

            List<string> confFiles = dirInfoProcessor
                .GetQueryPathes(new List<string>() { _tempDir.FullName });

            var expected = _tempDir.GetFiles()
                .Where(x => x.Extension == ".config")
                .Select(x => x.FullName)
                .ToList();

            Assert.AreEqual(expected, confFiles);
        }

    }
}
