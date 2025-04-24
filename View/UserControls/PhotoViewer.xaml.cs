using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPFTutorial.View.UserControls
{

    public partial class PhotoViewer : UserControl
    {
        public PhotoViewer()
        {
            InitializeComponent();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeVisible(false);
        }

        public void OpenImage(string imagePath)
        {
            // Create a new BitmapImage
            BitmapImage bitmap = new BitmapImage();

            // Begin initialization
            bitmap.BeginInit();

            // Set the URI of the image file
            bitmap.UriSource = new Uri($"pack://application:,,,/pictures/{imagePath}");

            // End initialization
            bitmap.EndInit();

            picture.Source = bitmap;
            MakeVisible();
        }

        public void MakeVisible(bool visible = true)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this); // Access window and its elements
            if (visible)
            {
                mainWindow.picPanel.Visibility = Visibility.Visible;
            }
            else
            {
                mainWindow.picPanel.Visibility = Visibility.Hidden;
            }
        }
    }
}
