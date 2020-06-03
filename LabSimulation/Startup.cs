using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LabSimulation.Startup))]
namespace LabSimulation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
