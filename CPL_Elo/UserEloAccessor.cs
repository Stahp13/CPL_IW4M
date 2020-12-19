using System;
using System.Collections.Generic;
using System.Text;
using SharedLibraryCore.Interfaces;
using SharedLibraryCore;
using SharedLibraryCore.Database.Models;
using System.Threading.Tasks;

namespace CPL_Elo
{
    public class UserEloAccessor
    {
        private readonly IMetaService metaService;

        public UserEloAccessor() { }
        public UserEloAccessor(IMetaService _metaService) {
            metaService = _metaService;
        }

        virtual public int GetClientElo(EFClient client) {
            Task<EFMeta> getEloTask = metaService.GetPersistentMeta("Elo", client);
            getEloTask.Wait();
            EFMeta EloMeta = getEloTask.Result;
            if (EloMeta == null) {
                metaService.AddPersistentMeta("Elo", "1000", client).Wait();
                Console.WriteLine($"Initializing Elo for user: {client.Name} [{client.NetworkId}]");
                return 1000;
            }
            return Convert.ToInt32(EloMeta.Value);
        }

        public void SetElo(EFClient client, int Elo) {
            metaService.AddPersistentMeta("Elo", Elo.ToString(), client).Wait();
        }
    }
}
