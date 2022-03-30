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

            DrawStack(Colors.Red, 100, 120, 2);
            DrawStack(Colors.Green, 200, 120, 5);
            DrawStack(Colors.Blue, 300, 120, 7);
            DrawStack(Colors.Snow, 400, 120, 3);
            DrawStack(Colors.SaddleBrown, 500, 120, 1);
            DrawStack(Colors.Gold, 600, 120, 4);
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


        int strokeThickness = 3;
        int width = 80;
        int height = 30;
        int thick = 8;

        public void DrawStack(Color c, int x, int y, int count) {
            for(int i = 0; i < count; i++) {
                DrawChip(c, x, y - thick*i);
			}
		}

        public void DrawChip(Color c, int x, int y) {
            Circle(x, y+ thick, width, height, new SolidColorBrush(c));
            Rectangle(x, y+height/2, width, thick, new SolidColorBrush(c));
            Line(x, y + height/2, strokeThickness, thick);
            Line(x+ width - strokeThickness, y + height/2, strokeThickness, thick);
            Circle(x, y, width, height, new SolidColorBrush(c));
        }

        public void Line(int x, int y, int x2, int y2) {
            Rectangle(x, y, x2, y2, Brushes.Black, true);
        }

        public void Rectangle(int x, int y, int width, int height, Brush c, bool stroke = false) {
            Rectangle rect = new Rectangle() {
                Width = width,
                Height = height,
                StrokeThickness = strokeThickness,
                Fill = c
            };
            if (stroke) rect.Stroke = Brushes.Black;

            Canvas.Children.Add(rect);

            rect.SetValue(Canvas.LeftProperty, (double)x);
            rect.SetValue(Canvas.TopProperty, (double)y);
        }

        public void Circle(int x, int y, int width, int height, Brush c) {

            Ellipse circle = new Ellipse() {
                Width = width,
                Height = height,
                Stroke = Brushes.Black,
                StrokeThickness = strokeThickness,
                Fill = c
            };

            Canvas.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, (double)x);
            circle.SetValue(Canvas.TopProperty, (double)y);
        }
    }
}
