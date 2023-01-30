using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Splendor {
	public class Game {
		static int PLAYERS = 2;
		static int WIN_SCORE = 25;
		static int STARTING_CARD_COUNT = 5;


		public static Game Instance;
		View view;

		public List<Card>[] Decks = new List<Card>[3];

		public Card[][] Board = new Card[3][];
		public Dictionary<Gem, int> Gems = new Dictionary<Gem, int>();
		public Player[] Players;
		public int Turn;
		private Random Rand = new Random();

		public Game(View v) {
			Instance = this;
			view = v;

			//cards
			var deck = CardLoader.LoadCards();
			for (int i = 0; i < 3; i++) {
				Decks[i] = new List<Card>();
			}
			foreach (var c in deck) {
				Decks[c.tier].Add(c);
			}

			for (int i = 0; i < 3; i++) {
				Board[i] = new Card[4];
				for (int j = 0; j < 4; j++) {
					DrawToBoard(i, j);
				}
			}

			//gems
			foreach(Gem g in Enum.GetValues(typeof(Gem))) {
				Gems.Add(g, g == Gem.Gold ? 5 : 2 + (PLAYERS == 4 ? 5 : PLAYERS));
			}

			//players
			Players = new Player[PLAYERS];
			for(int i = 0; i < PLAYERS; i++) {
				Players[i] = new Player();
			}

			//starting cards
			for(int i = 0; i < STARTING_CARD_COUNT; i++) {
				foreach (var p in Players) {
					var c = Draw(Decks[0]);
					if (c == null) continue;
					p.Cards[c.gem].Add(c);
				}
			}
		}

		private void DrawToBoard(int tier, int place) {
			var deck = Decks[tier];
			var card = Draw(deck);
			Board[tier][place] = card; // can be null
		}

		private Card Draw(List<Card> deck) {
			if (deck.Count == 0) return null;
			int n = Rand.Next(deck.Count);
			var c = deck[n];
			deck.RemoveAt(n);
			return c;
		}

		public void CardClicked(Card c) {
			if (draw.Count > 0) return;
			for (int tier = 0; tier < 3; tier++) {
				for (int place = 0; place < 4; place++) {
					if (Board[tier][place] == c) {
						Player p = Players[Turn];
						if (p.Afford(c)) {
							var payment = p.Buy(c);
							foreach(Gem g in payment.Keys) {
								Gems[g] += payment[g];
							}
							CheckWin();
							DrawToBoard(tier, place);
							NextTurn();
							view?.Redraw();
							return;
						}
					}
				}
			}
		}

		List<Gem> draw = new List<Gem>();
		bool coinLimit;
		public void GemClicked(Gem g) {
			if (coinLimit) {
				ReturnGem(g);
			}
			else {
				GrabGem(g);
			}
		}

		public void ReturnGem(Gem g) {
			var p = Players[Turn];
			if (p.Gems[g] == 0) return;
			Gems[g]++;
			p.Gems[g]--;
			NextTurn();
			view?.Redraw();
		}

		public void GrabGem(Gem g) {
			if (Gems[g] == 0) return;
			if (draw.Contains(g) && (Gems[g] < 3 || draw.Count > 1 || g == Gem.Gold)) return;
			if (g == Gem.Gold && draw.Count > 1) return;
			Gems[g]--;
			Players[Turn].Gems[g]++;
			draw.Add(g);
			if (draw.Count() == 3 || (draw.Count() == 2 && (draw[0] == draw[1] || draw[0] == Gem.Gold || draw[1] == Gem.Gold)) || NoMoves()) {
				NextTurn();
			}
			view?.Redraw();
		}

		public void NextTurn() {
			coinLimit = Players[Turn].CoinLimit();
			if (coinLimit) return;
			Turn = (Turn + 1) % Players.Length;
			draw.Clear();
		}

		public bool NoMoves() {
			foreach(Gem g in Gems.Keys) {
				if (Gems[g] > 0 && !draw.Contains(g)) return false;
			}
			if (draw.Count() == 1 && Gems[draw[0]] >= 3) return false;
			return true;
		}

		public void CheckWin() {
			if (Players[Turn].CheckWin(WIN_SCORE)) {
				view?.Redraw();
				MessageBox.Show($"Player {Turn+1} reached {WIN_SCORE} first.\nPlayer {Turn+1} Wins!");
				view?.StartNewGame();
			}
		}
	}
}
