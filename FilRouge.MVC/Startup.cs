using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FilRouge.MVC.Startup))]
namespace FilRouge.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
