using Microsoft.Owin;
using Owin;
using System.Net.Http.Formatting;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(ConfAsker.Startup))]
namespace ConfAsker
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
