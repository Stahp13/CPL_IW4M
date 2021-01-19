using PlayerRatingSystems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayerRatingCharterApp
{
    public partial class Form1 : Form
    {
        TeamBalancer teamBalancer;
        Random random;
        PlayerRatingSystem playerRatingSystem;

        public Form1() {
            InitializeComponent();
            random = new Random();
            teamBalancer = new TeamBalancer(8);
            playerRatingSystem = new EloRatingSystem(40);
        }

        IEnumerable<Player> generatePlayers(int count) {
            for(int i = 0; i < count; ++i) {
                double skill = random.NextDouble() * (double)(MaxPlayerSkill.Value - MinPlayerSkill.Value) + (double)MinPlayerSkill.Value;
                yield return new Player(skill);
            }
        }

        void chartPlayers(IEnumerable<Player> players) {
            var objChart = chart1.ChartAreas[0];
            objChart.AxisX.Minimum = (double)MinPlayerSkill.Value - 1.0;
            objChart.AxisX.Maximum = (double)MaxPlayerSkill.Value + 1.0;
            objChart.AxisX.Title = "Absolute Player Skill";
            objChart.AxisY.Minimum = players.Min(p => p.rating) - 100;
            objChart.AxisY.Maximum = players.Max(p => p.rating) + 100;
            objChart.AxisX.Title = "Player Rating";
            chart1.Series.Clear();
            String label = "for now unused label";
            chart1.Series.Add(label);
            chart1.Series[label].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            foreach (Player p in players.OrderBy(p => p.skill)) {
                chart1.Series[label].Points.AddXY(p.skill, p.rating);
            }
        }

        void simulateGames(IEnumerable<Player> players, int gamesCount) {
            int predictionSuccess = 0;
            if(MinPlayerSkill.Value > MaxPlayerSkill.Value) {
                return;
            }
            for (int i = 0; i < gamesCount; ++i) {
                var selectedPlayers = players.OrderBy(x => random.Next()).Take(8);
                var teams = teamBalancer.balanceTeams(selectedPlayers);
                bool result = teams.Key.Sum(p => p.skill) > teams.Value.Sum(p => p.skill);
                var teamsKeyRating = teams.Key.Sum(p => p.rating);
                var teamsValueRating = teams.Value.Sum(p => p.rating);
                bool presumedResult = teams.Key.Sum(p => p.rating) > teams.Value.Sum(p => p.rating);
                IEnumerable<Player> winningTeam = result ? teams.Key : teams.Value;
                IEnumerable<Player> losingTeam = result ? teams.Value: teams.Key;
                int ratingChange = playerRatingSystem.CalculateEloChange(winningTeam.Select(p => p.rating), losingTeam.Select(p => p.rating), 1.0);
                foreach(Player p in winningTeam) {
                    p.gamesPlayed++;
                    p.gamesWon++;
                    p.rating += ratingChange * (p.gamesPlayed < 5 ? 3 : (p.gamesPlayed < 10 ? 2 : 1));
                }
                foreach(Player p in losingTeam) {
                    p.gamesPlayed++;
                    p.gamesLost++;
                    p.rating -= ratingChange * (p.gamesPlayed < 5 ? 3 : (p.gamesPlayed < 10 ? 2 : 1));
                }
                if (result == presumedResult) {
                    predictionSuccess++;
                }
            }
            PredictionAccuracy.Text = ((double)predictionSuccess / gamesCount).ToString();
        }

        private void Simulate_Click(object sender, EventArgs e) {
            int playersCount = (int)PlayerCount.Value;
            int gamesCount = (int)GamesPlayed.Value;
            playerRatingSystem = new EloRatingSystem((int)kFactor.Value);
            var players = generatePlayers(playersCount).ToArray();
            simulateGames(players, gamesCount);

            chartPlayers(players);
            playerBindingSource.Clear();
            foreach (Player p in players) {
                playerBindingSource.Add(p);
            }
        }

        private void chart1_Click(object sender, EventArgs e) {

        }
    }
}
