using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Tests.Helpers
{
    public class FileSystemTestsHelper
    {
        //private const string _tempDirPath = "tempForTest";
        private readonly string _tempDirPath;

        //private const string _mainTestConfigFilename = @"\mainTestConfig.config";
        private readonly string _mainTestConfigFilename;

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

        public FileSystemTestsHelper(string tempDirPath, string mainTestConfigFilename)
        {
            _tempDirPath = tempDirPath;
            _mainTestConfigFilename = mainTestConfigFilename;
        }

        public DirectoryInfo CreateOrClearTempFolder()
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

        public void FillTempFolder()
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
    }
}
