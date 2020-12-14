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
        public int masterRankThreshold { get; set; }
        public int platinumRankThreshold { get; set; }
        public int goldRankThreshold { get; set; }
        public int silverRankThreshold { get; set; }
        public int bronzeRankThreshold { get; set; }

        public IBaseConfiguration Generate() {
            kFactor = Utilities.PromptInt("kFactor");
            masterRankThreshold = Utilities.PromptInt("masterRankThreshold");
            platinumRankThreshold = Utilities.PromptInt("platinumRankThreshold");
            goldRankThreshold = Utilities.PromptInt("goldRankThreshold");
            silverRankThreshold = Utilities.PromptInt("silverRankThreshold");
            bronzeRankThreshold = Utilities.PromptInt("bronzeRankThreshold");
            return this;
        }

        public string Name() => "EloConfiguration";
    }
}
