using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EulerExchangeAppDev.Startup))]
namespace EulerExchangeAppDev
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
