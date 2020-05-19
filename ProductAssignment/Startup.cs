using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProductAssignment.Startup))]
namespace ProductAssignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
