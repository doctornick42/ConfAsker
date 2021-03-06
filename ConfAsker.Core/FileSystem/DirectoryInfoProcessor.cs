﻿using ConfAsker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConfAsker.Core.FileSystem
{
    public class DirectoryInfoProcessor : IDirectoryInfoProcessor
    {
        public List<string> GetQueryPathes(IEnumerable<string> incommingStrings)
        {
            List<string> result = new List<string>();

            Regex configExtensionRegex = new Regex(@".*\.config$");

            foreach (var incommingStr in incommingStrings)
            {
                FileAttributes fAttr = File.GetAttributes(incommingStr);

                if (fAttr.HasFlag(FileAttributes.Directory))
                {
                    var fileNamesList = GetFiles(incommingStr)
                        .Where(x => configExtensionRegex.IsMatch(x));

                    foreach (var fileName in fileNamesList)
                    {
                        result.Add(String.Format("{0}\\{1}", incommingStr, fileName));
                    }
                }
                else
                {
                    result.Add(incommingStr);
                }
            }

            return result;
        }

        public List<string> GetDirectories(string path)
        {
            return GetFileSystemInfos(path, EFileSystemInfoType.Directories);
        }

        public List<string> GetFiles(string path)
        {
            return GetFileSystemInfos(path, EFileSystemInfoType.Files);
        }

        private List<string> GetFileSystemInfos(string path, EFileSystemInfoType type)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            var result = new List<string>();

            IEnumerable<FileSystemInfo> fileSystemInfos = new List<FileSystemInfo>();
            switch (type)
            {
                case EFileSystemInfoType.Directories:
                    fileSystemInfos = directory.GetDirectories();
                    break;
                case EFileSystemInfoType.Files:
                    fileSystemInfos = directory.GetFiles();
                    break;
            }

            if (fileSystemInfos.Any())
            {
                result.AddRange(fileSystemInfos.Select(x => x.Name));
            }
            
            return result;
        }
    }

    public enum EFileSystemInfoType
    {
        Files,
        Directories
    }
}
