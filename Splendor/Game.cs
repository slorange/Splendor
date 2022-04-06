﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splendor {
	public class Game {
		public static Game Instance;
		View view;

		public List<Card>[] Decks = new List<Card>[3];

		public Card[][] Board = new Card[3][];
		public Dictionary<Gem, int> Gems = new Dictionary<Gem, int>();
		public Player[] Players;
		public int Turn;
		private Random Rand = new Random();

		public Game(View v, int players) {
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
					Draw(i, j);
				}
			}

			//gems
			foreach(Gem g in Enum.GetValues(typeof(Gem))) {
				Gems.Add(g, g == Gem.Gold ? 5 : 7);
			}

			//players
			Players = new Player[players];
			for(int i = 0; i < players; i++) {
				Players[i] = new Player();
			}
		}

		private void Draw(int tier, int place) {
			var deck = Decks[tier];
			if (deck.Count == 0) {
				Board[tier][place] = null;
				return;
			}
			int n = Rand.Next(deck.Count);
			Board[tier][place] = deck[n];
			deck.RemoveAt(n);
		}

		public void CardClicked(Card c) {
			for (int tier = 0; tier < 3; tier++) {
				for (int place = 0; place < 4; place++) {
					if (Board[tier][place] == c) {
						Player p = Players[Turn];
						if (p.Afford(c)) {
							p.Buy(c);
							Draw(tier, place);
							view?.Redraw();
							NextTurn();
							return;
						}
					}
				}
			}
		}

		public void GemClicked(Gem g) {
			Gems[g]--;
			Players[Turn].Gems[g]++;
			NextTurn(); //TODO 3 or 2 coins per turn
			view?.Redraw();
		}

		public void NextTurn() {
			Turn = (Turn + 1) % Players.Length;
		}
	}
}
