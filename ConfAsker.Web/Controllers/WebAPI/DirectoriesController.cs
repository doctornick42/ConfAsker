using ConfAsker.Models.Directories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConfAsker.Controllers.WebAPI
{
    [RoutePrefix("api/directories")]
    public class DirectoriesController : ApiController
    {
        [HttpGet, Route("get")]
        public async Task<List<FileSystemUIItem>> Get(string path = "")
        {
            DirectoriesAndFilesList vm = new DirectoriesAndFilesList(path);
            return vm.GetList();
        }
    }
}
