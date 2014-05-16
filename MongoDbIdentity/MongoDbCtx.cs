using AspNet.Identity.MongoDB;
using MongoDB.Driver;
using MongoDbIdentity.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MongoDbIdentity
{
    public class MongoDbCtx
    {
        static ServerAndDb _serverAndDb;
        static MongoClient Client;
        MongoDatabase Database;
        public MongoCollection<ApplicationUser> Users { get { return Database.GetCollection<ApplicationUser>("users"); } }

        public IdentityContext IdentityContext { get { return new IdentityContext(Users); } }

        static MongoDbCtx()
        {
            _serverAndDb = new ServerAndDb(GetConnectionString());
            Client = new MongoClient(_serverAndDb.Server);
        }


        public MongoDbCtx()
        {
            Database = Client.GetServer().GetDatabase(_serverAndDb.Database);
        }

        #region Utils
        private static string GetConnectionString()
        {

            return WebConfigurationManager.AppSettings.Get("MONGOHQ_URL") ??
                  WebConfigurationManager.AppSettings.Get("MONGOLAB_URI") ??
                  WebConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString;
        }

        #endregion



    }


    class ServerAndDb
    {
        public string Server { get; set; }
        public string Database { get; set; }

        public ServerAndDb(string fullConnectionString)
        {
            Trace.WriteLine("Preparing url parsing for: " + fullConnectionString);
            if (!string.IsNullOrEmpty(fullConnectionString))
            {
                var slashIndex = fullConnectionString.LastIndexOf('/');
                Server = fullConnectionString;
                if (slashIndex < fullConnectionString.Length)
                    Database = fullConnectionString.Substring(++slashIndex);

                Trace.WriteLine("Parser done! Server: " + Server + ", Db: " + Database);
            }

        }
    }
}