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
		public Dictionary<Gem, int> Buy(Card c) {
			var payment = new Dictionary<Gem, int>();
			payment[Gem.Gold] = 0;
			foreach (Gem g in c.cost.Keys) {
				var cost = Math.Max(c.cost[g] - Cards[g].Count, 0);
				payment[g] = Math.Min(cost, Gems[g]);
				payment[Gem.Gold] += Math.Max(cost - Gems[g], 0);
			}
			foreach(Gem g in payment.Keys) {
				Gems[g] -= payment[g];
			}
			Cards[c.gem].Add(c);
			return payment;
		}
	}
}
