using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerRatingCharterApp
{
    class TeamBalancer
    {
        private static bool NextCombination(IList<int> num, int n, int k) {
            bool finished;

            var changed = finished = false;

            if (k <= 0) return false;

            for (var i = k - 1; !finished && !changed; i--) {
                if (num[i] < n - 1 - (k - 1) + i) {
                    num[i]++;

                    if (i < k - 1)
                        for (var j = i + 1; j < k; j++)
                            num[j] = num[j - 1] + 1;
                    changed = true;
                }
                finished = i == 0;
            }

            return changed;
        }

        private static IEnumerable<IEnumerable<int>> Combinations(IEnumerable<int> elements, int k) {
            var elem = elements.ToArray();
            var size = elem.Length;

            if (k > size) yield break;

            var numbers = new int[k];

            for (var i = 0; i < k; i++)
                numbers[i] = i;

            do {
                yield return numbers.Select(n => elem[n]);
            } while (NextCombination(numbers, size, k));
        }

        private IEnumerable<IEnumerable<int>> indicesCombinations;
        private IEnumerable<int> indices;

        public TeamBalancer(int lobbySize) {
            indices = Enumerable.Range(0, lobbySize);
            indicesCombinations = Combinations(indices, lobbySize / 2);
        }

        public KeyValuePair<IEnumerable<Player>, IEnumerable<Player>> balanceTeams(IEnumerable<Player> players) {
            Player[] playersArray = players.ToArray();
            int targetElo = players.Sum(p => p.rating) / 2;
            int bestTeamEloDiff = int.MaxValue;
            IEnumerable<int> bestIndices = new int[0];
            foreach (IEnumerable<int> indicesCombination in indicesCombinations){
                int sum = indicesCombination.Sum(i => playersArray[i].rating);
                int diff = Math.Abs(targetElo - sum);
                if(diff < bestTeamEloDiff) {
                    diff = bestTeamEloDiff;
                    bestIndices = indicesCombination;
                }
            }
            IEnumerable<int> otherIndices = indices.Where(i => !bestIndices.Contains(i));
            return new KeyValuePair<IEnumerable<Player>, IEnumerable<Player>>(bestIndices.Select(i => playersArray[i]), otherIndices.Select(i => playersArray[i]));
        }
    }
}
