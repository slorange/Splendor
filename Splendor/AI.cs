using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Splendor {
	public class AI : Player {
		Game game;
		public AI(Game g) {
			game = g;
		}

		public void Move() {
			Thread.Sleep(500);

			//buy card if can afford it
			var cards = new List<Card>();
			for (int tier = 0; tier < 3; tier++) {
				for (int place = 0; place < 4; place++) {
					var c = game.Board[tier][place];
					if (c == null) continue;
					cards.Add(c);
				}
			}
			cards.Sort((a, b) => b.score.CompareTo(a.score)); //order by most valuable
			foreach (var c in cards) {
				if (!Afford(c)) continue;
				game.CardSelected(c);
				return;
			}

			//calculate gem need based on total cost of all cards
			Dictionary<Gem, int> GemNeed = CalculateGemNeed();


			//divide gem need by current affordability + 1
			foreach(var gem in GemNeed.Keys.ToList()) {
				GemNeed[gem] /= 1 + Cards[gem].Count + Gems[gem];
			}

			//buy gems with biggest need
			List<Gem> SortedGems = GemNeed.OrderByDescending(x => x.Value).Select(x => x.Key).ToList(); //OpenAI OP

			var count = 0;
			foreach (var g in SortedGems) {
				if (count == 3) return;
				if (game.Gems[g] > 0) {
					game.GrabGem(g);
					count++;
					Thread.Sleep(200);
				}
			}

			//TODO check if coin count > 10, then choose coins to return
		}

		private Dictionary<Gem, int> CalculateGemNeed() {
			var GemNeed = new Dictionary<Gem, int>();
			GemNeed[Gem.Emerald] = 0;
			GemNeed[Gem.Diamond] = 0;
			GemNeed[Gem.Onyx] = 0;
			GemNeed[Gem.Ruby] = 0;
			GemNeed[Gem.Sapphire] = 0;

			for (int tier = 0; tier < 3; tier++) {
				for (int place = 0; place < 4; place++) {
					var c = game.Board[tier][place];
					if (c == null) continue;
					foreach (var x in c.cost) {
						GemNeed[x.Key] += x.Value;
					}
				}
			}

			return GemNeed;
		}
	}
}
