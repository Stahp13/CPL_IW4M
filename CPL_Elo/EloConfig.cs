using System;
using System.Collections.Generic;
using System.Text;
using SharedLibraryCore;
using SharedLibraryCore.Interfaces;

namespace CPL_Elo
{
    public class EloConfig : IBaseConfiguration
    {
        public int kFactor { get; set; }

        public IBaseConfiguration Generate() {
            kFactor = Utilities.PromptInt("Require privileged client login");
            return this;
        }

        public string Name() => "EloConfiguration";
    }
}
