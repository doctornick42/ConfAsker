﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerOnline.Models.Directories.FileSystemUIItems
{
    public class FileUIItem : FileSystemUIItem
    {
        public FileUIItem()
        {
            Type = Core.EFileSystemInfoType.Files;
        }
    }
}
