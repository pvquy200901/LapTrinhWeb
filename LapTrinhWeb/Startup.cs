using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LapTrinhWeb.StartupOwin))]

namespace LapTrinhWeb
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
