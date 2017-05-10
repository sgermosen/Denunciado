using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DenunciadoWeb.Startup))]
namespace DenunciadoWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
