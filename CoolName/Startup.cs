using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CoolName.Startup))]
namespace CoolName
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
