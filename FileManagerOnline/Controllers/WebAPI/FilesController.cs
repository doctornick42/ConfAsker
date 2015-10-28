using FileManagerOnline.Models.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FileManagerOnline.Controllers.WebAPI
{
    [RoutePrefix("api/files")]
    public class FilesController : ApiController
    {
        [HttpGet, Route("get")]
        public async Task<FileContent> Get(string path)
        {
            FileContent fileContent = new FileContent(path);
            await fileContent.LoadFile();
            return fileContent;
        }

        public async Task<string> GetConfigKey()
        {
            throw new NotImplementedException();
        }
    }
}
