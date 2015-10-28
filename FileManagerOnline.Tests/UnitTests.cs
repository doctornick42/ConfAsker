using System;
using NUnit.Framework;
using FileManagerOnline.Core;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace FileManagerOnline.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private const string _tempDirPath = "tempForTest";
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
        }

        [Test]
        public void TestConfigReader()
        {
            var testWebConfigPath1 = @"D:\projects\auditorius.tradingdesk\auditorius.tradingdesk\src\Auditorius.Tradingdesk.WebUI\Web.config";
            var configReader = new ConfigReader(testWebConfigPath1);

            var geoValue = configReader.GetAppSetting("GeoConnectionString");

            Assert.AreEqual("Geo", geoValue);
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
