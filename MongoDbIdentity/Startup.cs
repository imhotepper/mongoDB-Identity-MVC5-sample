using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MongoDbIdentity.Startup))]
namespace MongoDbIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
