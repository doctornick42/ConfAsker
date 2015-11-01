using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Models.Directories.FileSystemUIItems
{
    public class DirectoryUIItem : FileSystemUIItem
    {
        public DirectoryUIItem()
        {
            Type = Core.EFileSystemInfoType.Directories;
        }

        public string Path { get; set; }
    }
}
