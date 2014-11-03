using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RavenSignalRTest.Startup))]

namespace RavenSignalRTest
{
    public partial class Startup
    {
        //Orwin StartUp configuration 
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
