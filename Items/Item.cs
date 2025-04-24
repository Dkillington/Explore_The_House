namespace WPFTutorial.Items
{
    public class Item
    {

        public ItemEnums itemEnum;
        public string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                }
            }
        } 
        public string description; 
        public bool inspected { get; set; } // If this item was inspected in the inventory menu or not

        // [Constructor]
        public Item(ItemEnums _itemEnum, string _name, string _description)
        {
            inspected = false;

            itemEnum = _itemEnum;
            name = _name;
            description = _description;
        }
    }

    public enum ItemEnums
    {
        none, // Null

        // [Keys]
        // Kitchen
        kitchenLadle,

        // Bedroom
        bedroomSideTableKey,
        oscarPic,
        groupPic,

        // Attic
        atticTableKey,
        atticChestKey,
        crowbar,

        // [Notes]
        // Hall
        committeeDismissalLetter,

        // Bedroom
        diaryEntry1,

        // Kitchen
        poisonRecipe,
        shoppingList,

        // Attic
        committeeWarningLetter,
        letterFromPeter,
        teleportationBookPage,
    }
}
