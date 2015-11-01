using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Core
{
    public class DirectoryInfoProcessor
    {
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
