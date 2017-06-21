﻿using System;
using Raven.Client.Documents;

namespace DemoServer.Helpers
{
    public static class DocumentStoreHolder
    {
        private static readonly Lazy<IDocumentStore> DocStore = new Lazy<IDocumentStore>(CreateNorthwindStore);
        private static readonly Lazy<IDocumentStore> MediaDocStore = new Lazy<IDocumentStore>(CreateMediaStore);

        public static IDocumentStore Store => DocStore.Value;

        public static IDocumentStore MediaStore => MediaDocStore.Value;

        public static string NorthwindDatabaseName { get; set; }
        public static string MediaDatabaseName { get; set; }

        public static string Url { get; set; }

        public static string ServerUrl => Store.Urls[0];

        public static void SetDbInfo(string url, string dbNameNorthwind, string dbNameMedia)
        {
            Url = "http://Raven-NUC-2:8080";
            NorthwindDatabaseName = dbNameNorthwind;
            MediaDatabaseName = dbNameMedia;
        }

        private static IDocumentStore CreateNorthwindStore()
        {
            var docStoreInit = new DocumentStore
            {
                Urls = new[] { Url },
                Database = NorthwindDatabaseName
            }.Initialize();

            return docStoreInit;
        }

        private static IDocumentStore CreateMediaStore()
        {
            var docStoreInit = new DocumentStore
            {
                Urls = new[] { Url },
                Database = MediaDatabaseName
            }.Initialize();

            return docStoreInit;
        }
    }
}
