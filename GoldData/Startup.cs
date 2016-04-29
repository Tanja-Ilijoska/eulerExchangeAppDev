using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoldData.Startup))]
namespace GoldData
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
