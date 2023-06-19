using System;
using System.Collections.Generic;
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

namespace Draw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var rand = new Random();
            int width = rand.Next(0, 500);
            int height = rand.Next(0, 500);
            byte red = (byte)rand.Next(256);
            byte green = (byte)rand.Next(256);
            byte blue = (byte)rand.Next(256);
            var rectangle = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = new SolidColorBrush(Color.FromRgb(red, green, blue))
            };

            double canvasWidth = MyCanvas.ActualWidth;
            double canvasHeight = MyCanvas.ActualHeight;
            double x = rand.NextDouble() * (canvasWidth - rectangle.Width);
            double y = rand.NextDouble() * (canvasHeight - rectangle.Height);
            
            Canvas.SetLeft(rectangle, x);
            Canvas.SetTop(rectangle, y);

            MyCanvas.Children.Add(rectangle);

            // Change the text of the button
            UpdateCanvasButton.Content = "Canvas Updated!";
        }

        private void ClearCanvasButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Clear the existing elements from the canvas
            MyCanvas.Children.Clear();
        }

        private void DrawEllipseButton_Click(object sender, RoutedEventArgs e)
        {
            var rand = new Random();
            int width = rand.Next(0, 500);
            int height = rand.Next(0, 500);
            byte red = (byte)rand.Next(256);
            byte green = (byte)rand.Next(256);
            byte blue = (byte)rand.Next(256);

            var circle = new Ellipse
            {
                Width = width,
                Height = height,
                Fill = new SolidColorBrush(Color.FromRgb(red, green, blue))
            };
            
            double canvasWidth = MyCanvas.ActualWidth;
            double canvasHeight = MyCanvas.ActualHeight;
            double x = rand.NextDouble() * (canvasWidth - circle.Width);
            double y = rand.NextDouble() * (canvasHeight - circle.Height);
            
            Canvas.SetLeft(circle, x);
            Canvas.SetTop(circle, y);

            MyCanvas.Children.Add(circle);
        }
    }
}