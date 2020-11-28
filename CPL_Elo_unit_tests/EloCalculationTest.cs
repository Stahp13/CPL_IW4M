using Microsoft.VisualStudio.TestTools.UnitTesting;
using CPL_Elo;
using SharedLibraryCore.Interfaces;
using Moq;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CPL_Elo_unit_tests
{
    [TestClass]
    public class EloCalculationTest
    {
        private Mock<IConfigurationHandlerFactory> configurationHandlerFactoryMock;
        private Mock<IDatabaseContextFactory> databaseContextFactoryMock;
        private Mock<ITranslationLookup> translationLookupMock;
        private Mock<IMetaService> metaServiceMock;
        private EloConfig config;

        public EloCalculationTest() {
            config = new EloConfig();
            config.kFactor = 40;
            configurationHandlerFactoryMock = new Mock<IConfigurationHandlerFactory>();
            Mock<IConfigurationHandler<EloConfig>> configurationHandlerMock = new Mock<IConfigurationHandler<EloConfig>>();
            configurationHandlerMock.Setup(handler => handler.Configuration()).Returns(config);
            configurationHandlerFactoryMock.Setup(factory => factory.GetConfigurationHandler<EloConfig>("EloPluginSettings")).Returns(configurationHandlerMock.Object);
            databaseContextFactoryMock = new Mock<IDatabaseContextFactory>();
            translationLookupMock = new Mock<ITranslationLookup>();
            metaServiceMock = new Mock<IMetaService>();
        }

        EloPlugin getPlugin() {
            return new EloPlugin(configurationHandlerFactoryMock.Object, databaseContextFactoryMock.Object, translationLookupMock.Object, metaServiceMock.Object);
        }

        bool isNear(double value, double expectedValue, double allowedDiff = 0.01) {
            return Math.Abs(value - expectedValue) < allowedDiff;
        }

        [TestMethod]
        public void TestEqualEloWinProbability() {
            EloPlugin plugin = getPlugin();
            int averageWinnersElo = 1100;
            int averageLosersElo = 1100;
            double win_probability = plugin.GetWinProbability(averageWinnersElo, averageLosersElo);
            Assert.AreEqual(win_probability, 0.5);
        }

        [TestMethod]
        public void Test100EloAdvantageWinProbability() {
            EloPlugin plugin = getPlugin();
            int averageWinnersElo = 1100;
            int averageLosersElo = 1000;
            double win_probability = plugin.GetWinProbability(averageWinnersElo, averageLosersElo);
            Assert.IsTrue(isNear(win_probability, 0.64));
        }

        [TestMethod]
        public void TestEqualEloChangeCalculation() {
            EloPlugin plugin = getPlugin();
            List<int> winnersElo = new List<int> { 1000, 1000, 1000, 1000 };
            List<int> losersElo = new List<int> { 1000, 1000, 1000, 1000 };
            int Elo_change = plugin.CalculateEloChange(winnersElo, losersElo);
            Assert.AreEqual(Elo_change, 20);
        }

        [TestMethod]
        public void Test100EloAdvantageChangeCalculation() {
            EloPlugin plugin = getPlugin();
            List<int> winnersElo = new List<int> { 1100, 1100, 1100, 1100 };
            List<int> losersElo = new List<int> { 1000, 1000, 1000, 1000 };
            int Elo_change = plugin.CalculateEloChange(winnersElo, losersElo);
            Assert.AreEqual(Elo_change, 14);
        }
    }
}