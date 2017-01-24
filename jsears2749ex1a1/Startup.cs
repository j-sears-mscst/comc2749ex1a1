using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(jsears2749ex1a1.Startup))]
namespace jsears2749ex1a1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
