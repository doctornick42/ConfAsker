using System;
using NUnit.Framework;
using FileManagerOnline.Core;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace FileManagerOnline.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private const string _tempDirPath = "tempForTest";

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
            File.Create(tempDir.FullName + @"\testFile1.txt");
            File.Create(tempDir.FullName + @"\testFile2.jpg");
            File.Create(tempDir.FullName + @"\testFile3.config");
            File.Create(subFolder.FullName + @"\subfolderFile.txt");

            CreateTestConfigFile(tempDir.FullName, _mainTestConfigFilename);
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

        [Test]
        public void TestConfigReader()
        {
            DirectoryInfo tempDir = CreateOrClearTempFolder();
            FillTempFolder();

            var configReader = new ConfigReader(tempDir + _mainTestConfigFilename);

            var secondValue = configReader.GetAppSetting("secondKey");
            Assert.AreEqual("secondValue", secondValue);
        }

        [Test]
        public void TestGetDirectoriesAndFilesList()
        {
            DirectoryInfo tempDir = CreateOrClearTempFolder();
            FillTempFolder();

            DirectoryInfoProcessor dirInfoProcessor = new DirectoryInfoProcessor();
            var directories = dirInfoProcessor.GetDirectories(tempDir.FullName);
            var files = dirInfoProcessor.GetFiles(tempDir.FullName);

            var expectedOrderedDirectories = tempDir.GetDirectories()
                .Select(x => x.Name)
                .OrderBy(x => x);

            var expectedOrderedFiles = tempDir.GetFiles()
                .Select(x => x.Name)
                .OrderBy(x => x);

            Assert.AreEqual(expectedOrderedDirectories, 
                directories.ToList().OrderBy(x => x));
            Assert.AreEqual(expectedOrderedFiles, 
                files.ToList().OrderBy(x => x));

        }

    }
}
