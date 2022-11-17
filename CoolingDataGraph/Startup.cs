using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CoolingDataGraph.Startup))]
namespace CoolingDataGraph
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
