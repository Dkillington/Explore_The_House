using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFTutorial.Items;

namespace WPFTutorial.View.UserControls
{
    public partial class LootPanel : UserControl
    {
        // [Variables]
        private ObservableCollection<Item> containedItems = new ObservableCollection<Item>(); // A self-updating list of items saved to this panel
        public StorageEnums storageToLoot = StorageEnums.none; // Storage looted in this panel


        // [Constructor]
        public LootPanel()
        {
            InitializeComponent();

            storageView.ItemsSource = containedItems;
        }



        // [Buttons]
        // When mouse is double clicked
        private void storageView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TakeSelectedItems();
        }

        // Take selected
        private void takeBtn_Click(object sender, RoutedEventArgs e)
        {
            TakeSelectedItems();
        }

        // Exit
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            ToggleLoot();
        }



        // [Functions]
        // A public function that allows a specific Storage unit to be looted
        public void LootThis(StorageEnums storageEnum)
        {
            storageToLoot = storageEnum;
            ToggleLoot();
        }

        // Toggles loot panel
        public void ToggleLoot()
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this); // Access window and its elements
            if (mainWindow != null)
            {
                if (PanelIsHidden() && StorageExistsAndNotEmpty())
                {
                    OpenLoot();
                }
                else
                {
                    CloseLoot();
                }

                // Returns whether or not panel is open
                bool PanelIsHidden()
                {
                    if (mainWindow.lootPanel.Visibility == Visibility.Hidden)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                // Returns whether or not storage is real
                bool StorageExistsAndNotEmpty()
                {
                    // If storage is assigned to NULL . . .
                    if (storageToLoot == StorageEnums.none)
                    {
                        return false;
                    }
                    else
                    {
                        // If storage is empty . . .
                        if (mainWindow.allStorage[storageToLoot].items.Count <= 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }

            // Open the loot panel and populates data
            void OpenLoot()
            {
                RefreshPanel(); // Provide data

                ShowLootMenu();

                // Show the panel itself
                void ShowLootMenu()
                {
                    mainWindow.lootPanel.Visibility = Visibility.Visible;
                }
            }

            // Close loot panel and remove data
            void CloseLoot()
            {
                Visibility = Visibility.Hidden; // Hide inventory
                containedItems.Clear(); // Clear all inventory data
                storageToLoot = StorageEnums.none;
            }
        }

        // Refreshes all data within the panel
        private void RefreshPanel()
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this); // Access window and its elements


            if (mainWindow != null)
            {
                if(storageToLoot!=StorageEnums.none)
                {
                    GetTitle();
                    PopulateLootData();
                }

                void GetTitle()
                {
                    title.Text = mainWindow.allStorage[storageToLoot].name;
                }
                // Fill the panel item data with the storage container items
                void PopulateLootData()
                {
                    containedItems.Clear();

                    foreach (ItemEnums itemEnum in mainWindow.allStorage[storageToLoot].items)
                    {
                        Item item = mainWindow.allItems[itemEnum]; // Grab the item itself
                        containedItems.Add(item); // Add the item to the loot data
                    }
                }
            }
        }

        // Take player's selections and transfer data accordingly
        private void TakeSelectedItems()
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this); // Access window and its elements

            List<Item> takenItems = SaveSelectedItems();
            TakeItems();
            RemoveItemsFromStorage();
            RemoveTakenItems();

            // If theres still items available refresh, otherwise close out
            if(containedItems.Count > 0)
            {
                RefreshPanel();
            }
            else
            {
                ToggleLoot();
            }

            List<Item> SaveSelectedItems()
            {
                List<Item> takenItems = new List<Item>();
                foreach (Item item in storageView.SelectedItems)
                {
                    takenItems.Add(item);
                }

                return takenItems;
            }
            // Remove the items the player took from the corresponding storage container
            void RemoveItemsFromStorage()
            {
                foreach (Item item in takenItems)
                {
                    if (mainWindow.allStorage[storageToLoot].items.Contains(item.itemEnum))
                    {
                        mainWindow.allStorage[storageToLoot].items.Remove(item.itemEnum);
                    }
                }
            }

            // Put the items into player inventory
            void TakeItems()
            {
                foreach (Item item in takenItems)
                {
                    mainWindow.allStorage[StorageEnums.inventory].items.Add(item.itemEnum);
                }
            }

            // Remove the items that were taken
            void RemoveTakenItems()
            {
                foreach (Item item in takenItems)
                {
                    if (containedItems.Contains(item))
                    {
                        containedItems.Remove(item);
                    }
                }
            }
        }

    }
}

