using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rover.Startup))]
namespace Rover
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
