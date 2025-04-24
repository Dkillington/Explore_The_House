using System.Windows;
using System.Windows.Controls;

namespace WPFTutorial.View.UserControls
{
    public partial class EndPanel : UserControl
    {
        MainWindow mainWindow;
        public EndPanel()
        {
            mainWindow = (MainWindow)Window.GetWindow(this); // Access window and its elements
            InitializeComponent();
        }

        public void ShowPanel()
        {
            Visibility = Visibility.Visible;
        }
        public void HidePanel()
        {
            Visibility = Visibility.Hidden;
        }

        private void exitBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
