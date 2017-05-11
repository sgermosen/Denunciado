using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DenunciadoBackEnd.Startup))]
namespace DenunciadoBackEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
