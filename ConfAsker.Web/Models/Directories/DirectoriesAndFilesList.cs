using ConfAsker.Core;
using ConfAsker.Models.Directories.FileSystemUIItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConfAsker.Models.Directories
{
    public class DirectoriesAndFilesList
    {
        private readonly string _path;

        public DirectoriesAndFilesList(string path)
        {
            _path = String.IsNullOrEmpty(path)
                ? HttpContext.Current.Request.PhysicalApplicationPath
                : path;
        }

        public List<FileSystemUIItem> GetList()
        {
            DirectoryInfoProcessor dirInfoProcessor = new DirectoryInfoProcessor();
            var directories = from d in dirInfoProcessor.GetDirectories(_path)
                              select new DirectoryUIItem() 
                              {
                                  Name = d
                              };
            var files = from f in dirInfoProcessor.GetFiles(_path)
                        select new FileUIItem() 
                        {
                            Name = f
                        };

            List<FileSystemUIItem> result = new List<FileSystemUIItem>();
            if (directories.Any())
            {
                result.AddRange(directories);
            }
            if (files.Any())
            {
                result.AddRange(files);
            }

            return result;
        }
    }
}
