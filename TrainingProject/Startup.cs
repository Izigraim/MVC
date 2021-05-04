using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrainingProject.Startup))]
namespace TrainingProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
