using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using RavenSignalRTest.Models;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace RavenSignalRTest.Services
{
    [HubName("signalRTestHub")]
    public class SignalRTestHub : Hub
    {
        //Singleton customers object 
        private static List<Customer> customers = new List<Customer>();

        //First method that gets called when SignalR hub starts
        public override System.Threading.Tasks.Task OnConnected()
        {
            getCustomers();
            return base.OnConnected();
        }

        //Method that executes when hub reconnects
        public override System.Threading.Tasks.Task OnReconnected()
        {
            getCustomers();
            return base.OnReconnected();
        }
        //List of all customers (All Raven Shards)
        public void getCustomers()
        {
            IDocumentSession RavenSession = MvcApplication.Store.OpenSession();
            customers = RavenSession.Query<Customer>().ToList();

            //Broadcast to clients
            Send();

        }

        //Add a new customer
        public void addCustomer(Customer customer)
        {
            IDocumentSession RavenSession = MvcApplication.Store.OpenSession();
            RavenSession.Store(customer);
            RavenSession.SaveChanges();

            //All new customer to the singleton list and broadcast new customer to all clients
            customers.Add(customer);
            Send(customer);
        }

        //Broadcast all customers
        public void Send()
        {
            // Call the addNewMessageToPage method to update clients.
            var context = GlobalHost.ConnectionManager.GetHubContext<SignalRTestHub>();
            context.Clients.All.allCustomers(customers);
        }

        //Overloaded method to send one customer
        public void Send(Customer customer)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<SignalRTestHub>();
            var newCustomer = new List<Customer>();
            newCustomer.Add(customer);
            context.Clients.All.allCustomers(newCustomer);
        }
    }
}