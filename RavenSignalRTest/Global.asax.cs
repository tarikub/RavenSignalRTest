using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using Raven.Client.Embedded;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Client.Shard;
using Raven.Client;
using RavenSignalRTest.Models;

namespace RavenSignalRTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IDocumentStore Store;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //File based sharding example for customer data
            var shards = new Dictionary<string, IDocumentStore>
                {
                    {"North America", new EmbeddableDocumentStore { DataDirectory = "App_Data\\North-America" }},
                    {"Europe", new EmbeddableDocumentStore { DataDirectory = "App_Data\\Europe" }},
                    {"Asia", new EmbeddableDocumentStore { DataDirectory = "App_Data\\Asia" }},
                    {"Australia", new EmbeddableDocumentStore { DataDirectory = "App_Data\\Australia" }},
                };

            var shardStrategy = new ShardStrategy(shards)
                 .ShardingOn<Customer>(customer => customer.Region);

            Store = new ShardedDocumentStore(shardStrategy).Initialize();

            IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), Store);

        }
    }
}
