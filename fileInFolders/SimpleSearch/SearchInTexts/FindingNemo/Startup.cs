using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FindingNemo.Startup))]
namespace FindingNemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
