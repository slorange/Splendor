using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splendor {
	class CardLoader {

		public static List<Card> LoadCards() {

			var deck = new List<Card>();

			var all = File.ReadAllLines("../../Cards.txt");
			foreach(var l in all) {
				var a = l.Split('\t');
				var cost = new Dictionary<Gem, int>();
				if (a[9] != "") cost.Add(Gem.Onyx, int.Parse(a[9]));
				if (a[8] != "") cost.Add(Gem.Ruby, int.Parse(a[8]));
				if (a[7] != "") cost.Add(Gem.Emerald, int.Parse(a[7]));
				if (a[6] != "") cost.Add(Gem.Sapphire, int.Parse(a[6]));
				if (a[5] != "") cost.Add(Gem.Diamond, int.Parse(a[5]));
				var c = new Card(int.Parse(a[0]), a[2] == "" ? 0 : int.Parse(a[2]), StringToGem(a[1]), cost);
				deck.Add(c);
			}

			//foreach (Card c in deck) {
			//	Trace.WriteLine(c);
			//}
			return deck;
		}

		public static Gem StringToGem(string s) {
			if (s == "black") return Gem.Onyx;
			if (s == "red") return Gem.Ruby;
			if (s == "green") return Gem.Emerald;
			if (s == "blue") return Gem.Sapphire;
			return Gem.Diamond;
		}
	}
}
