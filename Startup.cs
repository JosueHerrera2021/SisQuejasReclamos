using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SisQuejasReclamos.Startup))]
namespace SisQuejasReclamos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
