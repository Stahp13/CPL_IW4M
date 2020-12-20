using Microsoft.VisualStudio.TestTools.UnitTesting;
using CPL_Elo;
using SharedLibraryCore.Interfaces;
using Moq;
using System.Collections.Generic;
using System;
using System.Linq;
using SharedLibraryCore;
using SharedLibraryCore.Database.Models;

namespace CPL_Elo_unit_tests
{
    [TestClass]
    public class LobbyDeserializationTest
    {
        private Mock<Server> serverMock;
        private Mock<EFClientFactory> clientFactoryMock;
        private List<EFClient> clientList;
        private Mock<UserEloAccessor> userEloAccessorMock;

        public LobbyDeserializationTest() {
            clientFactoryMock = new Mock<EFClientFactory>();
            clientList = new List<EFClient>();
            serverMock = new Mock<Server>();
            userEloAccessorMock = new Mock<UserEloAccessor>();
            userEloAccessorMock.Setup(handler => handler.GetClientElo(It.IsAny<EFClient>())).Returns(1000);

            clientFactoryMock.Setup(handler => handler.getClient(It.IsAny<long>())).Returns(new EFClient());

        }

        private EFClient createClient(long clientID) {
            EFClient ret = new EFClient();
            ret.NetworkId = clientID;
            return ret;
        }

        private string getGameString(string map, string mode, string axis, string allies) {
            return $"{{\"map\" : \"{map}\", \"mode\" : \"{mode}\", \"axis\" : {axis}, \"allies\" : {allies}}}";
        }

        private string getPlayerString(long userId, int kills, int deaths, int score, int captures, int defends, int plants, int defuses) {
            return $"{{\"userId\" : {userId}, \"kills\" : {kills}, \"deaths\" : {deaths}, \"score\" : {score}, " +
                $"\"captures\" : {captures}, \"defends\" : {defends}, \"plants\" : {plants}, \"defuses\" : {defuses}}}";
        }

        private string getTeamString(string score, double result, string players) {
            return $"{{\"score\" : \"{score}\", \"result\" : {result}, \"players\" : {players}}}";
        }

        [TestMethod]
        public void AxisWonHardpoint1v1() {
            string p1 = getPlayerString(0, 1, 2, 3, 4, 5, 6, 7);
            string p2 = getPlayerString(10, 11, 12, 13, 14, 15, 16, 17);
            string t1Players = $"[{p1}]";
            string t2Players = $"[{p2}]";
            string t1 = getTeamString("250", 1.0, t1Players);
            string t2 = getTeamString("220", 0.0, t2Players);
            string game = getGameString("Raid", "Hardpoint", t1, t2);
            Console.WriteLine(game);
            long[] ids = { 0, 10 };
            Lobby lobby = Lobby.create(clientFactoryMock.Object, userEloAccessorMock.Object, game);

            Assert.AreEqual(lobby.map, "Raid");
            Assert.AreEqual(lobby.mode, "Hardpoint");

            Assert.AreEqual(lobby.axis.name, "axis");
            Assert.AreEqual(lobby.axis.score, "250");
            Assert.AreEqual(lobby.axis.result, 1.0);
            Assert.AreEqual(lobby.axis.players.Length, 1);
            Assert.AreEqual(lobby.axis.players[0].userId, 0);
            Assert.AreEqual(lobby.axis.players[0].kills, 1);
            Assert.AreEqual(lobby.axis.players[0].deaths, 2);

            Assert.AreEqual(lobby.allies.name, "allies");
            Assert.AreEqual(lobby.allies.score, "220");
            Assert.AreEqual(lobby.allies.result, 0.0);
            Assert.AreEqual(lobby.allies.players.Length, 1);
            Assert.AreEqual(lobby.allies.players[0].userId, 10);
            Assert.AreEqual(lobby.allies.players[0].kills, 11);
            Assert.AreEqual(lobby.allies.players[0].deaths, 12);
        }
        
        [TestMethod]
        public void liveStringTest() {
            string p1 = getPlayerString(0, 1, 2, 3, 4, 5, 6, 7);
            string p2 = getPlayerString(10, 11, 12, 13, 14, 15, 16, 17);
            string t1Players = $"[{p1}]";
            string t2Players = $"[{p2}]";
            string t1 = getTeamString("250", 1.0, t1Players);
            string t2 = getTeamString("220", 0.0, t2Players);
            string game = $"{{ \"map\":\"Raid\",\"mode\":\"Hardpoint\",\"axis\":{{ \"score\":\"0\",\"result\":0.0,\"players\":[{{\"userId\":39545,\"kills\":0,\"deaths\":0,\"score\":0,\"captures\":0,\"defends\":0,\"plants\":0,\"defuses\":0}}]}},\"allies\":{{\"score\":\"0\",\"result\":1.0,\"players\":[{{\"userId\":2715,\"kills\":0,\"deaths\":0,\"score\":0,\"captures\":0,\"defends\":0,\"plants\":0,\"defuses\":0}}]}}}}";
            Console.WriteLine(game);
            long[] ids = { 0, 10 };
            Lobby lobby = Lobby.create(clientFactoryMock.Object, userEloAccessorMock.Object, game);

            Assert.AreEqual(lobby.map, "Raid");
            Assert.AreEqual(lobby.mode, "Hardpoint");

            Assert.AreEqual(lobby.axis.name, "axis");
            //Assert.AreEqual(lobby.axis.score, "250");
            Assert.AreEqual(lobby.axis.result, 0.0);
            Assert.AreEqual(lobby.axis.players.Length, 1);
            //Assert.AreEqual(lobby.axis.players[0].userId, 0);

            Assert.AreEqual(lobby.allies.name, "allies");
            //Assert.AreEqual(lobby.allies.score, "220");
            Assert.AreEqual(lobby.allies.result, 1.0);
            Assert.AreEqual(lobby.allies.players.Length, 1);
            //Assert.AreEqual(lobby.allies.players[0].userId, 10);
        }
    }
}
