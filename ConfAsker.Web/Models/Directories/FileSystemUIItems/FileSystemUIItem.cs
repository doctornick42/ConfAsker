using ConfAsker.Core;
using ConfAsker.Core.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfAsker.Models.Directories
{
    public class FileSystemUIItem
    {
        public string Name { get; set; }
        public EFileSystemInfoType Type { get; set; }
    }
}
