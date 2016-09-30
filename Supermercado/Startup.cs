using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Supermercado.Startup))]
namespace Supermercado
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
