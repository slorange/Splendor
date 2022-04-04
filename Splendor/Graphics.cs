using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Splendor {
	class Graphics {
        Canvas canvas;
        public Graphics(Canvas c) {
            canvas = c;
		}
        /*public void DrawPng() {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            string dir = Directory.GetCurrentDirectory();
            bitmap.UriSource = new Uri(dir + @"\..\..\images\EmeraldTest.png");
            bitmap.EndInit();
            Image image = new Image();
            image.Width = 100;
            image.Source = bitmap;

            Canvas.Children.Add(image);
        }*/


        int strokeThickness = 2;
        int width = 60;
        int height = 30;
        int thick = 8;

        MouseButtonEventHandler handler;

        private void DrawStackClicked() {
            MessageBox.Show("DrawStackClicked");
        }


        public void DrawCard(int x, int y, Card c) {
            handler = (sender, e) => { c.OnClick(); };

            strokeThickness = 3;
            RoundedRectangle(x, y, 100, 150, GemToCardBrush(c.gem), 10);
            strokeThickness = 2;
            DrawText(c.score, x + 10, y, 30, Brushes.Black);
            int i = 0;
            foreach(var cost in c.cost) {
                int y2 = y +120 - i * 28;
                Circle(x+5, y2, 25, 25, GemToBrush(cost.Key));
                Brush text = cost.Key == Gem.Diamond ? Brushes.Black : Brushes.White;
                DrawText(cost.Value, x+12, y2-3, 20, text);
                i++;
            }
        }

        public void DrawStack(Color c, int x, int y, int count) {
            handler = (sender, e) => { DrawStackClicked(); };

            for (int i = 0; i < count; i++) {
                DrawChip(c, x, y - thick * i);
            }
        }

        private Brush GemToCardBrush(Gem g) {
            if (g == Gem.Diamond) return Brushes.Snow;
            if (g == Gem.Emerald) return Brushes.LightGreen;
            if (g == Gem.Onyx) return Brushes.Tan;
            if (g == Gem.Ruby) return Brushes.LightCoral;
            if (g == Gem.Sapphire) return Brushes.SkyBlue;
            return null;
        }

        private Brush GemToBrush(Gem g) {
            if (g == Gem.Diamond) return Brushes.Snow;
            if (g == Gem.Emerald) return Brushes.Green;
            if (g == Gem.Gold) return Brushes.Gold;
            if (g == Gem.Onyx) return Brushes.SaddleBrown;
            if (g == Gem.Ruby) return Brushes.Red;
            if (g == Gem.Sapphire) return Brushes.Blue;
            return null;
        }

        private void DrawChip(Color c, int x, int y) {
            Circle(x, y + thick, width, height, new SolidColorBrush(c));
            Rectangle(x, y + height / 2, width, thick, new SolidColorBrush(c));
            VLine(x, y + height / 2, thick);
            VLine(x + width - strokeThickness, y + height / 2, thick);
            Circle(x, y, width, height, new SolidColorBrush(c));
        }
        private void DrawText(int s, int x, int y, int size, Brush b) {
            DrawText(s.ToString(), x, y, size, b);
        }

        private void DrawText(string s, int x, int y, int size, Brush b) {
            TextBlock tb = new TextBlock();
            tb.Text = s;
            tb.Foreground = b;
            tb.FontSize = size;
            tb.MouseLeftButtonDown += handler;

            canvas.Children.Add(tb);
            Canvas.SetLeft(tb, x);
            Canvas.SetTop(tb, y);
        }

        private void RoundedRectangle(int x, int y, int width, int height, Brush c, int round) {
            int cround = round * 2;
            Circle(x, y, cround, cround, c);
            Circle(x + width - cround, y, cround, cround, c);
            Circle(x, y + height - cround, cround, cround, c);
            Circle(x + width - cround, y + height - cround, cround, cround, c);

            HLine(x + round, y, width - cround);
            HLine(x + round, y + height - strokeThickness, width - cround);
            VLine(x, y + round, height - cround);
            VLine(x + width - strokeThickness, y + round, height - cround);

            Rectangle(x + round, y + strokeThickness, width - cround, height - 2 * strokeThickness, c);
            Rectangle(x + strokeThickness, y + round, width - 2 * strokeThickness, height - cround, c);
        }

        private void HLine(int x, int y, int l) {
            Line(x, y, l, strokeThickness);
        }

        private void VLine(int x, int y, int l) {
            Line(x, y, strokeThickness, l);
        }

        private void Line(int x, int y, int x2, int y2) {
            Rectangle(x, y, x2, y2, Brushes.Black, true);
        }

        private void Rectangle(int x, int y, int width, int height, Brush c, bool stroke = false) {
            Rectangle rect = new Rectangle() {
                Width = width,
                Height = height,
                StrokeThickness = strokeThickness,
                Fill = c
            };
			rect.MouseLeftButtonDown += handler;

            if (stroke) rect.Stroke = Brushes.Black;

            canvas.Children.Add(rect);

            rect.SetValue(Canvas.LeftProperty, (double)x);
            rect.SetValue(Canvas.TopProperty, (double)y);
        }


		private void Circle(int x, int y, int width, int height, Brush c) {

            Ellipse circle = new Ellipse() {
                Width = width,
                Height = height,
                Stroke = Brushes.Black,
                StrokeThickness = strokeThickness,
                Fill = c
            };
            circle.MouseLeftButtonDown += handler;

            canvas.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, (double)x);
            circle.SetValue(Canvas.TopProperty, (double)y);
        }
    }
}
