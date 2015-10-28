using FileManagerOnline.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerOnline.Models.Files
{
    public class FileContent
    {
        public readonly string _path;

        public FileContent(string path)
        {
            _path = path;
        }

        public string Name { get; set; }

        public string FileText { get; set; }

        public async Task LoadFile()
        {
            FileInfo fileInfo = new FileInfo(_path);
            Name = fileInfo.Name;
            using (var reader = fileInfo.OpenText())
            {
                FileText = await reader.ReadToEndAsync();
            }
        }

        public string GetConfigKey(string key)
        {
            ConfigReader configReader = new ConfigReader(_path);
            return configReader.GetAppSetting(key);
        }

        public string GetConnectionString(string key)
        {
            ConfigReader configReader = new ConfigReader(_path);
            return configReader.GetConnectionsString(key);
        }

        public string FindInConfig(string key)
        {
            string result = GetConnectionString(key);
            if (string.IsNullOrEmpty(result))
            {
                result = GetConfigKey(key);
            }
            return result;
        }
    }
}
