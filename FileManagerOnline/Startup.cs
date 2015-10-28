using Microsoft.Owin;
using Owin;
using System.Net.Http.Formatting;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(FileManagerOnline.Startup))]
namespace FileManagerOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            //ConfigureAuth(app);
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
        }
    }
}
