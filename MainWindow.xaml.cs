using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WPFTutorial.Items;

namespace WPFTutorial
{
    public partial class MainWindow : Window
    {
        // Lore Strings
        public string operationName = "Project Phantom's Gate";
        public string committeeName = "Bastow Committee of Scientific Endeavours";
        public string groupName = "Omega Research Group";
        public string protag_first = "Lewis";
        public string protag_last = "Sinclair";

        // Object Dictionaries/Lists
        public Dictionary<AreaEnum, Area> allAreas = new Dictionary<AreaEnum, Area>();
        public Dictionary<SoundEnums, Sound> allSounds = new Dictionary<SoundEnums, Sound>();
        public Dictionary<StorageEnums, Storage> allStorage = new Dictionary<StorageEnums, Storage>();
        public Dictionary<ItemEnums, Item> allItems = new Dictionary<ItemEnums, Item>();
        private List<Sound> activeSounds = new List<Sound>();

        // Other Variables
        private Area? currentArea = new Area(); 

        // [Constructor]
        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen; // Center the application
            InitializeComponent();

            // Populate Game
            InitalizeSounds();
            InitalizeItems();
            InitalizeContainers();
            InitalizeAreas();

            invBtn.Show(); // Show inventory button
            ChangeArea(AreaEnum.front); // Establish player at front of house to start

            DEVELOPERTOOLS();


            // Add all sounds to the game
            void InitalizeSounds()
            {
                foreach (SoundEnums value in Enum.GetValues(typeof(SoundEnums)))
                {
                    if (value != SoundEnums.none)
                    {
                        allSounds.Add(value, new Sound(value));
                    }
                }
            }
            // Add all items to the game
            void InitalizeItems()
            {
                // Hall
                {
                    CreateNote(ItemEnums.committeeDismissalLetter, "Notice of Dismissal", "A clean white letter found within a gray envelope", $"Dr. {protag_last},\n\nEffective immediately, you are dismissed from the committee for negligent conduct. While we openly advocate for creativity and the exploration of new ideas, your theories have been seen as wildly delusional - " +
                        $"and your experiments incredibly dangerous. We cannot allow this behavior to continue in this environment, and as such must revoke your title of Lead Scientist. We hope you understand.\n\nSincerely,\nJohn Bastow\nChairman of the {committeeName}");

                    CreateNote(ItemEnums.committeeWarningLetter, "Warning of Dismissal", "A dirty crumpled letter", $"Dr. {protag_last},\n\nThis is your final warning to cease your dangerous experiments if you wish you to maintain your title as Lead Scientist. The incident of August 12th involving your apprentice Dr. Lewis was not only dangerous, but incredibly foolish. " +
                        $"While Dr. Lewis is expected to make a full recovery, we must be certain that these wild experiments of yours will end, or else you will be removed from your position in the committee.\n\nSincerely,\nJohn Bastow\nChairman of the {committeeName}");

                    CreateNote(ItemEnums.letterFromPeter, "Letter From Peter", "A neatly folded white letter", $"{protag_first},\n\nAs your friend and colleague, I must advise you to stop making such mad claims and conducting such careless experiments. " +
                        $"While I respect your determination in your scientific pursuits, Robert's injury served as a grim reminder that the wellbeing of those around you comes second to your own goals. " +
                        $"Furthermore, these ideas and 'indisputable facts' you've discussed in recent meetings goes beyond usual knowledge or common sense - to the point where the committee has begun to question your own sanity and mental health. I believe it is within your own best interests to reevaluate your beliefs and conduct, or else begin drafting your resignation." +
                        $"\n\nSincerely,\nPeter Keaton\nAssociate Scientist, {committeeName},\nBranch of Research & Development");

                    CreateKey(ItemEnums.atticTableKey, "Worn Key", "A small key, very used and worn");
                }


                // Kitchen
                {
                    CreateKey(ItemEnums.kitchenLadle, "Wooden Ladle", "A wooden ladle used for soup");
                    CreateNote(ItemEnums.poisonRecipe, "Recipe", "A note scribbled on a piece of paper", "Potent Poison: \n- Salt (1 Cup)\n- Tetrodotoxin (1 mg)\n- Arsenic powder (2 tsp) \n- Botuli-\n*Page Scribbled Over* ");
                    CreateNote(ItemEnums.shoppingList, "Shopping List", "A torn sheet of paper detailing what looks like a shopping list", "- Oscilloscope\n- 500ft of RG-6/U cable\n- Spectrum Analyzer (May be able to restore old one?)");
                }

                // Bedroom
                {
                    CreateKey(ItemEnums.atticChestKey, "Silver Key", "A silver key with an oddly shaped square tip");
                    CreateNote(ItemEnums.diaryEntry1,
                        "Diary Page",
                        $"A diary entry of {protag_first} {protag_last}",
                    $"August 20th,\n\nIt watches me from the other side, calling to me. There is no silence anymore-only its demonic voices in my head.\n\nI believed opening the gate would bring knowledge, advancement... salvation. Instead, I have awoken something ancient. Something unholy.\n\nToday, I enter the gate to end it. If anyone is reading this, please know that I tried.\n\nGod forgive me.");
                    CreatePic(ItemEnums.oscarPic, "Picture of Oscar", "A picture of Oscar the Cat", "cat.jpg");
                    CreatePic(ItemEnums.groupPic, "Group Photo", "A grainy image of 3 scientists posing for a photo", "group.png");
                }

                // Attic
                {
                    CreateKey(ItemEnums.bedroomSideTableKey, "Ivory Key", "A small ivory key");
                    CreateKey(ItemEnums.crowbar, "Metal Crowbar", "A crowbar coated in red rust");
                    CreateNote(ItemEnums.teleportationBookPage, "\"Science Digest\" (Pg 15)", "A page torn from a science magazine", "\"Teleportation, once confined to the realm of science fiction, now emerges as a tangible prospect, guided by the principles of quantum entanglement. " +
                        "The entangled particles, existing in a state of quantum superposition, defy conventional logic as their properties remain intertwined across vast distances. Through this mystical connection, teleportation transcends physical boundaries, propelling particles and potentially information itself across the quantum landscape.\"");
                }

                // Notes
                void CreatePic(ItemEnums itemEnum, string name, string description, string path)
                {
                    allItems.Add(itemEnum, new Photograph(itemEnum, name, description, path));
                }
                void CreateKey(ItemEnums itemEnum, string name, string description)
                {
                    allItems.Add(itemEnum, new Key(itemEnum, name, description));
                }
                void CreateNote(ItemEnums itemEnum, string name, string description, string text)
                {
                    allItems.Add(itemEnum, new Note(itemEnum, name, description, text));
                }
            }
            // Add all containers to the game
            void InitalizeContainers()
            {
                // [Player's Inventory]
                allStorage.Add(StorageEnums.inventory, new Storage("Inventory", new List<ItemEnums> { }));

                // Hall
                allStorage.Add(StorageEnums.coatPocket, new Storage("Coat Pocket", new List<ItemEnums> { ItemEnums.atticTableKey, ItemEnums.committeeDismissalLetter }));

                // Bedroom
                allStorage.Add(StorageEnums.bedroomDrawer, new Storage("Bedroom Drawer", new List<ItemEnums> { ItemEnums.atticChestKey, ItemEnums.diaryEntry1, ItemEnums.oscarPic}));
                allStorage.Add(StorageEnums.bedroomChest, new Storage("Bedroom Chest", new List<ItemEnums> { ItemEnums.groupPic}));

                // Kitchen
                allStorage.Add(StorageEnums.kitchenCabinet, new Storage("Kitchen Cabinet", new List<ItemEnums> { ItemEnums.kitchenLadle }));
                allStorage.Add(StorageEnums.upperKitchenCabinet, new Storage("Upper Kitchen Cabinet", new List<ItemEnums> { ItemEnums.poisonRecipe, ItemEnums.shoppingList }));

                // Attic
                allStorage.Add(StorageEnums.atticTable, new Storage("Small Drawer", new List<ItemEnums> { ItemEnums.bedroomSideTableKey, ItemEnums.committeeWarningLetter, ItemEnums.letterFromPeter }));
                allStorage.Add(StorageEnums.atticChest, new Storage("Attic Chest", new List<ItemEnums> { ItemEnums.crowbar, ItemEnums.teleportationBookPage}));

                // Basement


            }
            // Add all areas to the game
            void InitalizeAreas()
            {
                NewArea(AreaEnum.front, new List<Button> { frontDoorBtn, frontExitBtn, stormClue, houseClue}, new List<SoundEnums> { SoundEnums.rainThunder1, SoundEnums.creepyWind, SoundEnums.thunder1}); // Front of the house
                NewArea(AreaEnum.hall, new List<Button> { leaveBtn, bedroomBtn, kitchenBtn, atticBtn, floorboardsBtn, coatBtn, hallClue1, hallClue2, hallClue3}, new List<SoundEnums> { SoundEnums.ominous1}); // Main hallway of house
                NewArea(AreaEnum.bedroom, new List<Button> { leaveBedroomBtn, underbedBtn, sideTableBtn }, new List<SoundEnums> { SoundEnums.rainEffect }); // Master bedroom of the house
                NewArea(AreaEnum.underbed, new List<Button> { leaveUnderbedBtn, boxBtn }, new List<SoundEnums> { SoundEnums.rainEffect }); // Under Bed In Bedroom
                NewArea(AreaEnum.kitchen, new List<Button> { leaveKitchenBtn, inspectPotBtn, openKitchenCabinetBtn, openUpperKitchenCabinetBtn, kitchenClue1, kitchenClue2, kitchenClue3}, new List<SoundEnums> { SoundEnums.rainEffect }); // Kitchen
                NewArea(AreaEnum.pot, new List<Button> {  leavePotBtn, usePotBtn }, new List<SoundEnums> { SoundEnums.rainEffect }); // Pot in Kitchen
                NewArea(AreaEnum.attic, new List<Button> { leaveAtticBtn, atticChestBtn, atticDrawerBtn, atticClue1, atticClue2, atticClue3, atticClue4, atticClue5, atticClue6, atticClue7}, new List<SoundEnums> { SoundEnums.rainEffect }); // Attic
                NewArea(AreaEnum.basement, new List<Button> { leaveBasementBtn, accessLabBtn, basementClue1 }, new List<SoundEnums> { SoundEnums.coldWind }); // Basement 
                NewArea(AreaEnum.lab, new List<Button> { leaveLabBtn, accessPortalBtn, labClue1, labClue2}, new List<SoundEnums> { SoundEnums.lab1, SoundEnums.lab2}); // Lab 


                // Function used to simplify the area creation process
                void NewArea(AreaEnum area, List<Button> activeButtons, List<SoundEnums> activeSounds)
                {
                    allAreas.Add(area, new Area(area, activeButtons, activeSounds));
                }
            }


            void DEVELOPERTOOLS()
            {
                VerifyAllItemsAreStored();
                //AddAllItemsToInventory();

                // Throw an error if any items aren't stored in game containers
                void VerifyAllItemsAreStored()
                {
                    List<ItemEnums> unstoredItems = ReturnUnstoredItems();
                    if (unstoredItems.Count > 0)
                    {
                        string message = "";
                        foreach (ItemEnums itemEnum in unstoredItems)
                        {
                            message += itemEnum.ToString() + ", ";
                        }
                        throw new Exception("ERROR, you forgot to add the following items to game containers: " + message);
                    }

                    List<ItemEnums> ReturnUnstoredItems()
                    {
                        List<ItemEnums> unstoredEnums = new List<ItemEnums>();

                        // For every item . . .
                        foreach (ItemEnums itemEnum in allItems.Keys)
                        {
                            bool isStored = isInAnyStorage(itemEnum); // Find out if it is stored anywhere

                            // If the item is not stored in any in-game containers, add it to the 'UnstoredEnums' list
                            if (!isStored)
                            {
                                unstoredEnums.Add(itemEnum);
                            }
                        }

                        return unstoredEnums;


                        // Returns if the given item is stored anywhere
                        bool isInAnyStorage(ItemEnums itemEnum)
                        {
                            foreach (KeyValuePair <StorageEnums, Storage> storage in allStorage)
                            {
                                // If any storage besides inventory contains this item, return true 
                                if (storage.Value.items.Contains(itemEnum) && (storage.Key != StorageEnums.inventory))
                                {
                                    return true;
                                }
                            }

                            return false;
                        }

                    }
                }

                // Add all items to player inventory
                void AddAllItemsToInventory()
                {
                    // Add all items to inventory
                    foreach (ItemEnums item in allItems.Keys)
                    {
                        allStorage[StorageEnums.inventory].items.Add(item);
                    }
                }
            }
        }


        // [Functionality]
        // Switch to a different area/location, provide an entry sound effect
        void ChangeArea(AreaEnum area, SoundEnums entrySoundEnum = SoundEnums.none)
        {
            StopAllSounds(); // End all active sounds
            ClosePanelsIfOpen();

            Area? lastArea = currentArea;

            // Recieve all data for the selected room
            currentArea = allAreas[area];

            ChangeScene(currentArea.imgName.ToString()); // Change Background Image
            ShowButtons(); // Only show buttons relevant to the area
            ActivateSounds(currentArea.sounds); // Only activate sounds relevant to the area


            // Stop all current sounds
            void StopAllSounds()
            {
                StopEachSound();
                ClearList();

                // Go through each index of the activeSounds list and fully cancel/dispose of the sound object
                void StopEachSound()
                {
                    foreach (Sound sound in activeSounds)
                    {
                        sound.Stop();
                    }
                }

                // Clear all entries in the activeSounds list
                void ClearList()
                {
                    activeSounds.Clear();
                }
            }

            // Change the current scene
            void ChangeScene(string picPath)
            {
                Debug.WriteLine($"Grabbing background image '{picPath}'");
                try
                {
                    screenImg.Source = new BitmapImage(new Uri($"backgrounds/{picPath}.jpg", UriKind.Relative));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to map image, '{ex}'");
                }
            }

            void ShowButtons()
            {
                // Get all buttons in the MainGrid
                var buttons = MainGrid.Children.OfType<Button>();

                HideLastAreaButtons();
                ShowAreaButtons();

                void HideLastAreaButtons()
                {
                    // If the last area isn't null
                    if (lastArea != null)
                    {
                        // Hide all buttons in that area
                        foreach (Button button in lastArea.buttons)
                        {
                            button.Visibility = Visibility.Hidden;
                        }
                    }
                }

                void ShowAreaButtons()
                {
                    // Show only the specified buttons
                    foreach (var button in currentArea.buttons)
                    {
                        button.Visibility = Visibility.Visible;
                    }
                }
            }

            void ActivateSounds(List<SoundEnums> areaSounds)
            {
                // Add Sounds
                AddAreaSounds();
                AddEntrySounds();

                // Play Sounds
                PlaySounds();



                // Add only sounds associated with the area (Rain, wind, etc)
                void AddAreaSounds()
                {
                    foreach (SoundEnums soundEnum in areaSounds)
                    {
                        Sound sound = allSounds[soundEnum]; // Grab the actual Sound object
                        AddToList(sound); // Add it to the list of all active sounds
                    }
                }
                // Add only sounds associated with entry (A door squeak, metal door opening, etc)
                void AddEntrySounds()
                {
                    if (entrySoundEnum != SoundEnums.none)
                    {
                        Sound sound = allSounds[entrySoundEnum]; // Grab the actual Sound object
                        sound.repeats = false; // Set repeating to false (It is supposed to be an entry sound only)
                        activeSounds.Add(sound); // Add it to the list of all active sounds
                    }
                }

                // Add the individual sound to the list of active sounds
                void AddToList(Sound sound)
                {
                    activeSounds.Add(sound);
                }

                // Play all sounds
                void PlaySounds()
                {
                    foreach(Sound sound in activeSounds)
                    {
                        sound.Play();
                    }
                }
            }


            // If any panels are open close them
            void ClosePanelsIfOpen()
            {
                if (inv.Visibility == Visibility.Visible)
                {
                    inv.ToggleInventory();
                }
                if (lootPanel.Visibility == Visibility.Visible)
                {
                    lootPanel.ToggleLoot();
                }

                picPanel.MakeVisible(false); // Hide picture panel
                endPanel.HidePanel(); // Hide passcode panel
            }
        }

        // Loot a storage container
        void Loot(StorageEnums storageEnum)
        {
            if (PasswordRequired())
            {
            }
            else
            {
                lootPanel.LootThis(storageEnum);
            }


            bool PasswordRequired()
            {
                // If the given container doesn't have a password value (Null or white space variable), then no password is required
                if(string.IsNullOrWhiteSpace(allStorage[storageEnum].requiredPassword))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        // General Alert to the player
        void Alert(string message)
        {
            MessageBox.Show(message, "Alert!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        // Check you have item, provide error message if not
        bool HaveKey(ItemEnums requiredItem, string lockedMessage)
        {
            if (allStorage[StorageEnums.inventory].items.Contains(requiredItem))
            {
                Alert($"{allItems[requiredItem].name} used!");
                return true;
            }
            else
            {
                Alert(lockedMessage);
                return false;
            }

        }

        // Provide Clue
        private void ClueClick(object sender, RoutedEventArgs e)
        {
            ToolTip toolTip = new ToolTip();

            Button? senderBtn = sender as Button;
            if (senderBtn != null)
            {
                toolTip.Content = senderBtn.Content;
                toolTip.FontSize = 15;

                // Optionally, show the tooltip programmatically
                ShowToolTip(senderBtn, toolTip, 2000);
            }


            void ShowToolTip(Button button, ToolTip toolTip, int miliseconds = 1000)
            {
                // Position the tooltip (optional)
                toolTip.PlacementTarget = button;
                toolTip.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;

                toolTip.IsOpen = true;


                // Set the duration the tooltip stays open
                var timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(miliseconds) // Adjust the duration as needed
                };
                timer.Tick += (sender, args) =>
                {
                    toolTip.IsOpen = false;
                    timer.Stop();
                };
                timer.Start();
            }
        }

        // End Game
        private void EndGameClick(object sender, RoutedEventArgs e)
        {
            string? content = null;

            if (sender.GetType() == typeof(string))
            {
                string? text = sender as string;

                if(text != null)
                {
                    content = text;
                }
            }
            else
            {
                Button? btn = sender as Button;

                if(btn != null)
                {
                    content = btn.Content.ToString();
                }
            }


            MessageBox.Show(content, "Game Over", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            Environment.Exit(0);

        }


        // [Events / Areas]
        // Front of house
        private void frontDoorBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeArea(AreaEnum.hall, SoundEnums.doorSlam1);
        }

        // Hallway
        private void leaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(HaveKey(ItemEnums.none, "The door has locked behind you ..."))
            {
                ChangeArea(AreaEnum.front);
            }
        }
        private void kitchenBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeArea(AreaEnum.kitchen, SoundEnums.doorSqueak1);
        }
        private void bedroomBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeArea(AreaEnum.bedroom, SoundEnums.doorSqueak2);
        }
        private void atticBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeArea(AreaEnum.attic, SoundEnums.stairsWood1);
        }
        private void floorboardsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (HaveKey(ItemEnums.crowbar, "The floorboards are slightly loose here, revealing what looks like a small room below . . ."))
            {
                ChangeArea(AreaEnum.basement);
            }
        }
        private void coatBtn_Click(object sender, RoutedEventArgs e)
        {
            Loot(StorageEnums.coatPocket);
        }

        // Bedroom
        private void leaveBedroomBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeArea(AreaEnum.hall);
        }
        private void sideTableBtn_Click(object sender, RoutedEventArgs e)
        {
            if(HaveKey(ItemEnums.bedroomSideTableKey, "The drawer wont budge, but it does have a single key hole . . ."))
            {
                Loot(StorageEnums.bedroomDrawer);
            }
        }
        private void leaveUnderbedBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeArea(AreaEnum.bedroom);
        }
        private void boxBtn_Click(object sender, RoutedEventArgs e)
        {
            Loot(StorageEnums.bedroomChest);
        }
        private void underbedBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeArea(AreaEnum.underbed);
        }

        // Kitchen
        private void leaveKitchenBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeArea(AreaEnum.hall);
        }
        private void openKitchenCabinetBtn_Click(object sender, RoutedEventArgs e)
        {
            Loot(StorageEnums.kitchenCabinet);
        }
        private void inspectPotBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeArea(AreaEnum.pot);
        }
        private void openUpperKitchenCabinetBtn_Click(object sender, RoutedEventArgs e)
        {
            Loot(StorageEnums.upperKitchenCabinet);
        }

        // Pot
        private void leavePotBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeArea(AreaEnum.kitchen);
        }
        private void usePotBtn_Click(object sender, RoutedEventArgs e)
        {
            if (HaveKey(ItemEnums.kitchenLadle, "The soup looks very unappetizing"))
            {
                MessageBoxResult result = MessageBox.Show("Taste the strange liquid?", "Beware", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    EndGameClick("You begin to feel very lightheaded, then moments later collapse - DEAD!", new RoutedEventArgs());
                }
                else
                {

                }
            }
        }

        // Attic
        private void leaveAtticBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeArea(AreaEnum.hall, SoundEnums.stairsWood2);
        }
        private void atticChestBtn_Click(object sender, RoutedEventArgs e)
        {
            if (HaveKey(ItemEnums.atticChestKey, "The chest is locked with an odd latch, a silver keyhole in the shape of a square"))
            {
                Loot(StorageEnums.atticChest);
            }
        }
        private void atticDrawerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (HaveKey(ItemEnums.atticTableKey, "The small table is locked"))
            {
                Loot(StorageEnums.atticTable);
            }

        }

        // Basement
        private void leaveBasementBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeArea(AreaEnum.hall);
        }
        private void accessLabBtn_Click(object sender, RoutedEventArgs e)
        {
            if(HaveKey(ItemEnums.atticChestKey, "You need a key!"))
            {
                ChangeArea(AreaEnum.lab);
            }
        }

        // Lab
        private void leaveLabBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeArea(AreaEnum.basement);
        }
        private void accessPortalBtn_Click(object sender, RoutedEventArgs e)
        {
            endPanel.ShowPanel();
        }
    }
}
