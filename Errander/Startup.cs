using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Errander.Startup))]
namespace Errander
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
