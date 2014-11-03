using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;
using RavenSignalRTest.Models;

namespace RavenSignalRTest
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}