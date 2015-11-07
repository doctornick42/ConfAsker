using System;
using NUnit.Framework;
using ConfAsker.Core;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using ConfAsker.Core.FileSystem;
using ConfAsker.Core.Query;

namespace ConfAsker.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private const string _tempDirPath = "tempForTest";

        private DirectoryInfo _tempDir;

        private const string _mainTestConfigFilename = @"\mainTestConfig.config";
        private readonly List<string> _tempFiles = new List<string>() 
        {
            @"\testFile1.txt",
            @"\testFile2.jpg",
            @"\testFile3.config"
        };
        private readonly List<string> _tempSubfolders = new List<string>() 
        {
            "testSubfolder"
        };

        private DirectoryInfo CreateOrClearTempFolder()
        {
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            var tempDirectoriesArray = dir.GetDirectories(_tempDirPath);

            DirectoryInfo tempDirectory;

            if (tempDirectoriesArray.Length == 0)
            {
                dir.CreateSubdirectory(_tempDirPath);
                tempDirectory = dir.GetDirectories(_tempDirPath)[0];
            }
            else
            {
                tempDirectory = dir.GetDirectories(_tempDirPath)[0];
                tempDirectory.Delete(true);
                dir.CreateSubdirectory(_tempDirPath);
                tempDirectory = dir.GetDirectories(_tempDirPath)[0];
            }

            return tempDirectory;
        }

        private void FillTempFolder()
        {
            DirectoryInfo currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            var tempDirectoriesArray = currentDir.GetDirectories(_tempDirPath);
            DirectoryInfo tempDir;
            if (tempDirectoriesArray.Length == 0)
            {
                tempDir = CreateOrClearTempFolder();
            }
            else
            {
                tempDir = tempDirectoriesArray[0];
            }

            var subFolder = tempDir.CreateSubdirectory("testSubfolder");
            var testFile1 = File.Create(tempDir.FullName + @"\testFile1.txt");
            var testFile2 = File.Create(tempDir.FullName + @"\testFile2.jpg");
            var testFile3 = File.Create(tempDir.FullName + @"\testFile3.config");
            var subFolderFile = File.Create(subFolder.FullName + @"\subfolderFile.txt");

            try
            {
                CreateTestConfigFile(tempDir.FullName, _mainTestConfigFilename);
            }
            finally
            {
                testFile1.Close();
                testFile2.Close();
                testFile3.Close();
                subFolderFile.Close();
            }
        }

        private void CreateTestConfigFile(string path, string filename)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sb.AppendLine("<configuration>");

            sb.AppendLine("<configSections>");
            sb.AppendLine("</configSections>");

            sb.AppendLine("<connectionStrings>");
            sb.AppendLine("<add name=\"firstConnectionString\" connectionString=\"server=123.456.789.1,1234;initial catalog=test_db;user id=test_user;password=123456;multipleactiveresultsets=true;App=EntityFramework\" />");
            sb.AppendLine("<add name=\"secondConnectionString\" connectionString=\"server=123.456.789.1,1234;initial catalog=test_db_2;user id=test_user;password=123456;multipleactiveresultsets=true;App=EntityFramework\" />");
            sb.AppendLine("</connectionStrings>");

            sb.AppendLine("<appSettings>");
            sb.AppendLine("<add key=\"firstKey\" value=\"firstValue\" />");
            sb.AppendLine("<add key=\"secondKey\" value=\"secondValue\" />");
            sb.AppendLine("<add key=\"thirdKey\" value=\"thirdValue\" />");
            sb.AppendLine("</appSettings>");

            sb.AppendLine("</configuration>");

            File.WriteAllText(String.Concat(path, filename), sb.ToString());
        }

        [SetUp]
        public void SetUp()
        {
            _tempDir = CreateOrClearTempFolder();
            FillTempFolder();
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

    }
}
