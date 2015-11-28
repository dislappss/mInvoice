using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mInvoice.Startup))]
namespace mInvoice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
