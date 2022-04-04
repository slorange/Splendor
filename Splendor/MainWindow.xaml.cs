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
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();

            Graphics g = new Graphics(Canvas);

            g.DrawStack(Colors.Red, 100, 120, 2);
            g.DrawStack(Colors.Green, 200, 120, 5);
            g.DrawStack(Colors.Blue, 300, 120, 7);
            g.DrawStack(Colors.Snow, 400, 120, 3);
            g.DrawStack(Colors.SaddleBrown, 500, 120, 1);
            g.DrawStack(Colors.Gold, 600, 120, 4);
			var deck = CardLoader.LoadCards();

			int i = 0;
			foreach(var c in deck) {
				g.DrawCard(i%16*110, i/16*160, c);
				i++;
			}
		}
       
    }
}
