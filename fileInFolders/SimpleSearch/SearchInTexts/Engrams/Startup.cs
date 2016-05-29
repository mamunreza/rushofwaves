using Engrams;

using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Engrams
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
