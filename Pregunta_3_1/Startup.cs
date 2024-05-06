using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pregunta_3_1.Startup))]
namespace Pregunta_3_1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
