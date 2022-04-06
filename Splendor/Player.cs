using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splendor {
	public class Player {
		public Dictionary<Gem, int> Gems = new Dictionary<Gem, int>();
		public Dictionary<Gem, List<Card>> Cards = new Dictionary<Gem, List<Card>>();

		public Player() {
			foreach (Gem g in Enum.GetValues(typeof(Gem))) {
				Gems[g] = 0;
				Cards[g] = new List<Card>();
			}
		}

		public bool Afford(Card c) {
			var gold = Gems[Gem.Gold];
			foreach (Gem g in c.cost.Keys) {
				if (Gems[g] + Cards[g].Count >= c.cost[g]) continue;
				var missing = c.cost[g] - Gems[g] - Cards[g].Count;
				if (missing > gold) return false;
				gold -= missing;
			}
			return true;
		}

		//TODO: give player a choice whether or not to use gold coins. Might want to hoard a certain one to prevent other players.
		public void Buy(Card c) {
			Cards[c.gem].Add(c);
			foreach (Gem g in c.cost.Keys) {
				var cost = c.cost[g] - Cards[g].Count;
				if (Gems[g] >= cost) {
					Gems[g] -= cost;
				}
				else {
					Gems[g] = 0;
					Gems[Gem.Gold] -= cost - Gems[g];
				}
			}
		}
	}
}
