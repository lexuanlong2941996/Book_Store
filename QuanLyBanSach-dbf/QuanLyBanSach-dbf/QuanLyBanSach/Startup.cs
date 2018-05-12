using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuanLyBanSach.Startup))]
namespace QuanLyBanSach
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
