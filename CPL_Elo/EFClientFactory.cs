using System;
using System.Collections.Generic;
using System.Text;
using SharedLibraryCore;
using SharedLibraryCore.Database.Models;
using SharedLibraryCore.Interfaces;


namespace CPL_Elo
{
    public class EFClientFactory
    {
        Server server;

        public EFClientFactory() { }

        public EFClientFactory(Server _server) {
            server = _server;
        }
        public virtual EFClient getClient(long userId) {
            return server.GetClientsAsList().Find(c => c.NetworkId == userId);
        }
    }
}
