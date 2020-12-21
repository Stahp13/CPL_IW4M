using System;
using System.Collections.Generic;
using System.Text;
using SharedLibraryCore;
using SharedLibraryCore.Database.Models;
using SharedLibraryCore.Interfaces;
using SharedLibraryCore.Database;
using System.Linq;


namespace CPL_Elo
{
    public class EFClientFactory
    {
        DatabaseContext databaseContext;

        public EFClientFactory() { }

        public EFClientFactory(DatabaseContext _databaseContext) {
            databaseContext = _databaseContext;
        }
        public virtual EFClient getClient(long networkId) {
            return databaseContext.Clients.First(_client => _client.NetworkId == networkId);
        }
    }
}
