using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Splendor {
	public partial class View : Window {
		static int PLAYERS = 4;

		static Graphics graphics;
		static Game game;
		public View() {
			InitializeComponent();

			graphics = new Graphics(Canvas);
			game = new Game(this, PLAYERS);

			Redraw();
		}

		public void Redraw() {
			Canvas.Children.Clear();

			int k = 0;
			foreach (var kv in game.Gems) {
				graphics.DrawStack(kv.Key, 700+k*70, 120, kv.Value);
				k++;
			}

			for (int i = 0; i < 3; i++) {
				for (int j = 0; j < 4; j++) {
					var card = game.Board[i][j];
					if (card == null) continue;
					graphics.DrawCard(700 + j * 110, 200 + i * 160, card);
				}
			}

			int[] px = { 10, 1250, 1250, 10 };
			int[] py = { 820, 820, 410, 410 };
			for (int i = 0; i < game.Players.Length; i++) {
				var p = game.Players[i];
				var x = px[i];
				var y = py[i];

				int m = 0;
				foreach(var kv in p.Cards) {
					int n = kv.Value.Count-1;
					foreach (var c in kv.Value) {
						graphics.DrawCard(x + m * 110, y - n * 40 - 150, c);
						n--;
					}
					m++;
				}

				int l = 0;
				foreach (var kv in p.Gems) {
					graphics.DrawStack(kv.Key, 4 + x + l * 93, y, kv.Value);
					l++;
				}
			}
		}
	}
}
