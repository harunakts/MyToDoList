using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gorev_Yoneticisi.Startup))]
namespace Gorev_Yoneticisi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
