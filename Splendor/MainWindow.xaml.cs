using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Splendor {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();

            DrawPng();// DrawChip(Colors.Red, 100, 100);
		}
        public void DrawPng() {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            string dir = Directory.GetCurrentDirectory();
            bitmap.UriSource = new Uri(dir+@"\..\..\images\EmeraldTest.png");
            bitmap.EndInit();
            Image image = new Image();
            image.Width = 100;
            image.Source = bitmap;

            Canvas.Children.Add(image);
        }

        public void DrawChip(Color c, int x, int y) {

            Circle(x, y, 150, 75, new SolidColorBrush(c));
		}

        public void Circle(int x, int y, int width, int height, Brush c) {

            Ellipse circle = new Ellipse() {
                Width = width,
                Height = height,
                Stroke = Brushes.Black,
                StrokeThickness = 6,
                Fill = c
            };

            Canvas.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, (double)x);
            circle.SetValue(Canvas.TopProperty, (double)y);
        }
    }
}
