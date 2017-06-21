﻿using DemoServer.Controllers;
using DemoServer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;
using System;

namespace DemoServer.Demos.Advanced
{
    public class FailoverStoreHolder
    {
        private readonly static Lazy<IDocumentStore> _store =
            new Lazy<IDocumentStore>(CreateDocumentStore);

        private static IDocumentStore CreateDocumentStore()
        {
            var documentStore = new DocumentStore
            {
                Urls = new[]// urls of the nodes in the RavenDB Cluster
			{
                "http://Raven-Tablet-2:8080"
            },
                Database = "Demo",
            };

            documentStore.Initialize();
            return documentStore;
        }

        public static IDocumentStore Store
        {
            get { return _store.Value; }
        }
    }

    public partial class AdvancedController : BaseController
    {
        [HttpGet]
        [Route("/advanced/replicationFailover")]
        [Demo("ReplicationFailover", DemoOutputType.Flatten, demoOrder: 200)]
        public object ReplicationFailover(string id = "Users/1")
        {
            using (var session = FailoverStoreHolder.Store.OpenSession())
            {
                var results = session.Load<dynamic>(id);

                return results;
            }
            
        }
    }
}
