using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Splendor {
	public enum Gem {Ruby, Emerald, Sapphire, Diamond, Onyx, Gold};
	public class Card {
		public int tier;
		public int score;
		public Gem gem;
		public Dictionary<Gem, int> cost;

		public Card(int tier, int score, Gem gem, Dictionary<Gem, int> cost) {
			this.tier = tier-1;
			this.score = score;
			this.gem = gem;
			this.cost = cost;
		}

		public override string ToString() {
			return $"{gem} {score}";
		}
	}
}
