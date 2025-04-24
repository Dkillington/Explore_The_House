using System.Windows;
using System.Windows.Controls;

namespace WPFTutorial.View.UserControls
{
    public partial class InventoryBtn : UserControl
    {
        public InventoryBtn()
        {
            InitializeComponent();
        }

        private void inventoryBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this); // Access window and its elements

            // Toggle inventory
            if (mainWindow != null)
            {
                mainWindow.inv.ToggleInventory();
            }

        }


        public void Show()
        {
            Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            Visibility = Visibility.Hidden;
        }
    }
}
