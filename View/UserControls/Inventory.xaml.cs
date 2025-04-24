using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFTutorial.Items;

namespace WPFTutorial.View.UserControls
{
    public partial class Inventory : UserControl
    {
        // [Variables]
        private ObservableCollection<Item> containedItems = new ObservableCollection<Item>(); // A self-updating list of items saved to this panel

        // [Constructor]
        public Inventory()
        {
            InitializeComponent();

            descriptionView.Text = "";
            letterView.Text = "";
            storageView.ItemsSource = containedItems;
        }

        // [Buttons]
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            ToggleInventory();
        }
        private void storageView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewItems();
        }

        // [Functions]
        public void ToggleInventory()
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this); // Access window and its elements

            // Toggle inventory
            if (mainWindow != null)
            {
                // Toggle the visibility of the inventory window
                if (InventoryIsVisible())
                {
                    CloseInventory();
                }
                else
                {
                    OpenInventory();
                }
            }

            bool InventoryIsVisible()
            {
                // Toggle the visibility of the inventory window
                if (mainWindow.inv.Visibility == Visibility.Hidden)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            void OpenInventory()
            {
                CloseStorageIfOpen(); // If player is exploring a storage container, close the menu

                RefreshInventory(); // Fill the inventory's data

                ShowInventory(); // Show inventory


                void ShowInventory()
                {
                    mainWindow.inv.Visibility = Visibility.Visible;
                }

                void CloseStorageIfOpen()
                {
                    if(mainWindow.lootPanel.Visibility == Visibility.Visible)
                    {
                        mainWindow.lootPanel.Visibility = Visibility.Hidden;
                    }
                }
            }

            void CloseInventory()
            {
                Visibility = Visibility.Hidden; // Hide inventory
                containedItems.Clear(); // Clear all inventory data
                descriptionView.Text = "";
                letterView.Text = "";

                mainWindow.picPanel.MakeVisible(false); // Hide picture panel
                
            }
        }

        void RefreshInventory()
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this); // Access window and its elements
            containedItems.Clear();

            List<Item> tempList = new List<Item>(); // (Pre-sorted)
            foreach (ItemEnums itemEnum in mainWindow.allStorage[StorageEnums.inventory].items)
            {
                Item item = mainWindow.allItems[itemEnum]; // Grab the item itself
                tempList.Add(item); // Add the item
            }

            // Sort
            tempList.Sort((x, y) => string.Compare(x.Name, y.Name));

            // Add
            foreach (Item item in tempList)
            {
                containedItems.Add(item);
            }
        }

        // Controls how the item is viewed!
        private void ViewItems()
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this); // Access window and its elements

            // Refresh Texts to empty
            descriptionView.Text = "";
            letterView.Text = "";

            // Grab item type
            Item? item = storageView.SelectedItem as Item;
            Type? itemType = item?.GetType();
            descriptionView.Text = item?.description; // Set description text

            // If it is a note, display letter text
            if (itemType == typeof(WPFTutorial.Items.Note))
            {
                Note? theNote = item as Note;
                letterView.Text = theNote?.text;
            }
            else if (itemType == typeof(WPFTutorial.Items.Photograph))
            {
                Photograph? photo = item as Photograph;

                if (photo != null)
                {
                    mainWindow.picPanel.OpenImage(photo.path);
                }
            }
            
            if(item != null)
            {
                item.inspected = true; // Set item as inspected
            }

            RefreshInventory(); // Refresh (So 'inspected' item changes color)
        }
    }
}
