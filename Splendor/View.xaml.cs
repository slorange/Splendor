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

		static Graphics graphics;
		static View Instance;
		static Game game;
		public View() {
			InitializeComponent();

			graphics = new Graphics(Canvas);
			Instance = this;
			game = new Game(this, 4);

			Redraw();
		}

		public static void Redraw() {
			Instance.Canvas.Children.Clear();

			int x = 0;
			foreach (var kv in game.Gems) {
				graphics.DrawStack(kv.Key, 100+x*70, 120, kv.Value);
				x++;
			}

			for (int i = 0; i < 3; i++) {
				for (int j = 0; j < 4; j++) {
					var card = game.Board[i][j];
					if (card == null) continue;
					graphics.DrawCard(300 + j * 110, 200 + i * 160, card);
				}
			}
		}

	}
}
