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
			game = new Game(this);

			Redraw();
		}

		public static void Redraw() {
			Instance.Canvas.Children.Clear();

			graphics.DrawStack(Colors.Red, 100, 120, 2);
			graphics.DrawStack(Colors.Green, 200, 120, 5);
			graphics.DrawStack(Colors.Blue, 300, 120, 7);
			graphics.DrawStack(Colors.Snow, 400, 120, 3);
			graphics.DrawStack(Colors.SaddleBrown, 500, 120, 1);
			graphics.DrawStack(Colors.Gold, 600, 120, 4);
			var deck = CardLoader.LoadCards();

			int i = 0;
			foreach (var c in deck) {
				graphics.DrawCard(i % 16 * 110, i / 16 * 160, c);
				i++;
			}
		}

	}
}
