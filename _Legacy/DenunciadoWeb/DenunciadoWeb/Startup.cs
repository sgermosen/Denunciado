using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DenunciadoBackEnd.Startup2))]
namespace DenunciadoBackEnd
{
    public partial class Startup2
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
