using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AlkusStore.Startup))]
namespace AlkusStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
